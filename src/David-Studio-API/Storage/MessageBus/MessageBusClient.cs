using System.IO;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Storage.MessageBus
{
    public class MessageBusClient : IMessageBusClient
    {
        private IConnection _connection;
        private IModel _channel;

        public MessageBusClient(IConfiguration configuration)
        {
            ConnectionFactory factory = new()
            {
                Uri = new Uri(configuration.GetConnectionString("EventBus")!)
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare("portfolio", ExchangeType.Topic, durable: true);
        }

        public void CreateSubsciber(string eventSource, Func<string, string?, Task> eventExecuter)
        {
            string queueName = _channel.QueueDeclare().QueueName;

            _channel.QueueBind(queue: queueName,
                               exchange: "portfolio",
                               routingKey: $"{eventSource}.*");

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = JsonSerializer.Deserialize<string>(body);
                var routingKey = ea.RoutingKey;

                await eventExecuter(routingKey, message);
            };

            _channel.BasicConsume(queue: queueName,
                                  autoAck: true,
                                  consumer: consumer);
        }

        public void Dispose()
        {
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }
    }
}

