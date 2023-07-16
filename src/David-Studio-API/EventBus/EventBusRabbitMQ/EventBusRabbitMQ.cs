using System.Diagnostics.Tracing;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using EventBus;
using EventBus.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using static System.Formats.Asn1.AsnWriter;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public EventBusRabbitMQ(
            IRabbitMQPersistenConnection eventBusConnection,
            IEventBusSubscriptionsManager subsManager,
            IServiceProvider serviceProvider,
            ILogger<EventBusRabbitMQ> logger)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;

            _persistenConnection = eventBusConnection
                ?? throw new ArgumentNullException(nameof(_persistenConnection));
            _subsManager = subsManager;

            _consumerChannel = CreateConsumerChannel();
        }

        public void Publish<TEventSource, TEventAction, TData>
            (TEventSource source, TEventAction action, TData data)
            where TData : class
            where TEventSource : Enum
            where TEventAction : Enum
        {
            if (!_persistenConnection.IsConnected)
                _persistenConnection.TryConnect();

            byte[] body = JsonSerializer.SerializeToUtf8Bytes(data, data.GetType());
            string eventKey = _subsManager.GetEventKey(source, action);

            _logger.LogTrace("Creating RabbitMQ channel to publish event: {EventKey}", eventKey);

            using var channel = _persistenConnection.CreateModel();

            _logger.LogTrace("Declaring RabbitMQ exchange to publish event: {EventKey}", eventKey);

            channel.ExchangeDeclare(EXCHANGE_NAME, ExchangeType.Direct, durable: true);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            _logger.LogTrace("Publishing event to RabbitMQ: {EventKey}", eventKey);

            channel.BasicPublish(exchange: EXCHANGE_NAME,
                     routingKey: eventKey,
                     basicProperties: null,
            body: body);
        }

        public void Subscribe<TData, TEventHandler, TEventSource, TEventAction>
            (TEventSource source, TEventAction action)
            where TData : class
            where TEventHandler : IIntegrationEventHandler<TData>
            where TEventSource : Enum
            where TEventAction : Enum
        {
            string eventKey = _subsManager.GetEventKey(source, action);

            if (!_subsManager.HasSubscriptionForEvent(eventKey))
            {
                _logger.LogInformation("Subscribing to event {EventKey}", eventKey);

                _subsManager.AddSubscription<TData, TEventHandler>(eventKey);

                _consumerChannel.QueueBind(queue: _queueName,
                                       exchange: EXCHANGE_NAME,
                                       routingKey: eventKey);

                StartBasicConsume();
            }
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
            var eventKey = eventArgs.RoutingKey;
            var message = Encoding.UTF8.GetString(eventArgs.Body.Span);

            try
            {
                await ProcessEvent(eventKey, message);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error Processing message \"{Message}\"", message);

                return;
            }

            _consumerChannel.BasicAck(eventArgs.DeliveryTag, multiple: false);
        }

        private async Task ProcessEvent(string eventKey, string message)
        {
            _logger.LogTrace("Start processing RabbitMQ event: {EventKey}", eventKey);
            if (_subsManager.HasSubscriptionForEvent(eventKey))
            {
                await using var scope = _serviceProvider.CreateAsyncScope();
                var subscription = _subsManager.GetHandlerForEvent(eventKey);

                var handler = scope.ServiceProvider.GetService(subscription);

                var eventType = _subsManager.GetEventTypeByName(eventKey);
                var integrationEvent = JsonSerializer.Deserialize(message, eventType);
                var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);

                await Task.Yield();
                await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });
            }
            else
            {
                _logger.LogWarning("No subscription for RabbitMQ event: {EventKey}", eventKey);
            }
        }

        public void Dispose()
        {
            if (_consumerChannel != null)
                _consumerChannel.Dispose();
        }
    }
}
