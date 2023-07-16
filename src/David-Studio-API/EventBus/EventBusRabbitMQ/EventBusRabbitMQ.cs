using System.Diagnostics.Tracing;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EventBusRabbitMQ
{
    public class EventBusRabbitMQ : IEventBus, IDisposable
    {
        private const string EXCHANGE_NAME = "david-studio";

        private readonly IRabbitMQPersistenConnection _persistenConnection;
        private readonly ILogger<EventBusRabbitMQ> _logger;

        private IModel _consumerChannel;
        private string _queueName;

        public EventBusRabbitMQ(
            IRabbitMQPersistenConnection eventBusConnection,
            ILogger<EventBusRabbitMQ> logger)
        {
            _logger = logger;

            _persistenConnection = eventBusConnection
                ?? throw new ArgumentNullException(nameof(_persistenConnection));
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

            byte[] body = JsonSerializer.SerializeToUtf8Bytes(data);
            string eventSource = Enum.GetName(typeof(TEventSource), source)!.ToLower();
            string eventAction = Enum.GetName(typeof(TEventAction), action)!.ToLower();

            _logger.LogTrace("Creating RabbitMQ channel to publish event: {EventSource} ({EventAction})", eventSource, eventAction);

            using var channel = _persistenConnection.CreateModel();

            _logger.LogTrace("Declaring RabbitMQ exchange to publish event: {EventSource}", eventSource);

            channel.ExchangeDeclare(EXCHANGE_NAME, ExchangeType.Topic, durable: true);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            _logger.LogTrace("Publishing event to RabbitMQ: {EventSource}", eventSource);

            channel.BasicPublish(exchange: EXCHANGE_NAME,
                     routingKey: $"{eventSource}.{eventAction}",
                     basicProperties: null,
                     body: body);
        }

        public void Subscribe<TEventSource, TEventAction>
            (TEventSource source, TEventAction action)
            where TEventSource : Enum
            where TEventAction : Enum
        {
            string eventSource = Enum.GetName(typeof(TEventSource), source)!.ToLower();
            string eventAction = Enum.GetName(typeof(TEventAction), action)!.ToLower();

            _logger.LogInformation("Subscribing to event {EventSource}", eventSource);

            _consumerChannel.QueueBind(queue: _queueName,
                                       exchange: EXCHANGE_NAME,
                                       routingKey: $"{eventSource}.{eventAction}");

            StartBasicConsume();
        }

        private IModel CreateConsumerChannel()
        {
            if (!_persistenConnection.IsConnected)
                _persistenConnection.TryConnect();

            _logger.LogTrace("Creating RabbitMQ consumer channel");

            var channel = _persistenConnection.CreateModel();

            channel.ExchangeDeclare(EXCHANGE_NAME, ExchangeType.Topic, durable: true);

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
            var topic = eventArgs.RoutingKey;
            var message = Encoding.UTF8.GetString(eventArgs.Body.Span);

            try
            {
                await ProcessEvent(topic, message);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error Processing message \"{Message}\"", message);
            }

            _consumerChannel.BasicAck(eventArgs.DeliveryTag, multiple: false);
        }

        private async Task ProcessEvent(string topic, string message)
        {
            _logger.LogTrace("Processing RabbitMQ event: {Topic}", topic);
        }

        public void Dispose()
        {
            if (_consumerChannel != null)
                _consumerChannel.Dispose();
        }
    }
}
