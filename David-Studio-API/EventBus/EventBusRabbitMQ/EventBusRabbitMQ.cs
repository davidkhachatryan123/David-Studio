using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using EventBus;
using EventBus.Abstractions;
using EventBus.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace EventBusRabbitMQ
{
    public class EventBusRabbitMQ : IEventBus, IDisposable
    {
        private const string EXCHANGE_NAME = "david-studio";

        private readonly IRabbitMQPersistenConnection _persistenConnection;
        private readonly IEventBusSubscriptionsManager _subsManager;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<EventBusRabbitMQ> _logger;

        private IModel _consumerChannel;
        private string _queueName;

        private readonly int _retryCount;

        public EventBusRabbitMQ(
            IRabbitMQPersistenConnection eventBusConnection,
            IEventBusSubscriptionsManager subsManager,
            IServiceProvider serviceProvider,
            ILogger<EventBusRabbitMQ> logger,
            int retryCount = 5)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;

            _persistenConnection = eventBusConnection
                ?? throw new ArgumentNullException(nameof(_persistenConnection));
            _subsManager = subsManager;

            _consumerChannel = CreateConsumerChannel();

            _retryCount = retryCount;
        }

        public void Publish(IntegrationEvent @event)
        {
            if (!_persistenConnection.IsConnected)
                _persistenConnection.TryConnect();

            var policy = RetryPolicy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                    _logger.LogWarning(ex, "Could not publish event: {EventId} after {Timeout}s", @event.Id, $"{time.TotalSeconds:n1}");
                });

            string eventName = @event.GetType().Name;

            _logger.LogTrace("Creating RabbitMQ channel to publish event: {EventId} ({EventName})", @event.Id, eventName);
            using var channel = _persistenConnection.CreateModel();

            _logger.LogTrace("Declaring RabbitMQ exchange to publish event: {EventId}", @event.Id);
            channel.ExchangeDeclare(EXCHANGE_NAME, ExchangeType.Direct, durable: true);

            byte[] body = JsonSerializer.SerializeToUtf8Bytes(@event, @event.GetType());

            policy.Execute(() =>
            {
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                _logger.LogTrace("Publishing event to RabbitMQ: {EventId}", @event.Id);

                channel.BasicPublish(exchange: EXCHANGE_NAME,
                         routingKey: eventName,
                         basicProperties: null,
                         body: body);
            });
        }

        public void Subscribe<TEvent, TEventHandler>()
            where TEvent : IntegrationEvent
            where TEventHandler : IIntegrationEventHandler<TEvent>
        {
            string eventName = _subsManager.GetEventName<TEvent>();

            if (!_subsManager.HasSubscriptionForEvent(eventName))
            {
                _consumerChannel.QueueBind(queue: _queueName,
                                       exchange: EXCHANGE_NAME,
                                       routingKey: eventName);
            }

            _logger.LogInformation("Subscribing to event {EventName}", eventName);

            _subsManager.AddSubscription<TEvent, TEventHandler>();
            StartBasicConsume();
        }

        private IModel CreateConsumerChannel()
        {
            if (!_persistenConnection.IsConnected)
                _persistenConnection.TryConnect();

            _logger.LogTrace("Creating RabbitMQ consumer channel");

            var channel = _persistenConnection.CreateModel();

            channel.ExchangeDeclare(EXCHANGE_NAME, ExchangeType.Direct, durable: true);

            _queueName = channel.QueueDeclare().QueueName;

            channel.CallbackException += (sender, ea) =>
            {
                _logger.LogWarning(ea.Exception, "Recreating RabbitMQ consumer channel");

                _consumerChannel.Dispose();
                _consumerChannel = CreateConsumerChannel();
                StartBasicConsume();
            };

            return channel;
        }

        private void StartBasicConsume()
        {
            _logger.LogTrace("Starting RabbitMQ basic consume");

            if (_consumerChannel != null)
            {
                var consumer = new AsyncEventingBasicConsumer(_consumerChannel);

                consumer.Received += Consumer_Received;

                _consumerChannel.BasicConsume(
                    queue: _queueName,
                    autoAck: false,
                    consumer: consumer);
            }
            else
            {
                _logger.LogError("StartBasicConsume can't call on _consumerChannel == null");
            }
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs eventArgs)
        {
            var eventName = eventArgs.RoutingKey;
            var message = Encoding.UTF8.GetString(eventArgs.Body.Span);

            try
            {
                await ProcessEvent(eventName, message);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error Processing message \"{Message}\"", message);

                return;
            }

            _consumerChannel.BasicAck(eventArgs.DeliveryTag, multiple: false);
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            _logger.LogTrace("Start processing RabbitMQ event: {eventName}", eventName);

            if (_subsManager.HasSubscriptionForEvent(eventName))
            {
                await using var scope = _serviceProvider.CreateAsyncScope();
                SubscriptionInfo subscription = _subsManager.GetSubscribtion(eventName);

                var handler = scope.ServiceProvider.GetService(subscription.Handler);

                var integrationEvent = JsonSerializer.Deserialize(message, subscription.Event);
                var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(subscription.Event);

                await Task.Yield();
                await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });
            }
            else
                _logger.LogWarning("No subscription for RabbitMQ event: {eventName}", eventName);
        }

        public void Dispose()
        {
            if (_consumerChannel != null)
                _consumerChannel.Dispose();
        }
    }
}
