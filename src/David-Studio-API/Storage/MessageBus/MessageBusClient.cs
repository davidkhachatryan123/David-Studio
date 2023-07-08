using RabbitMQ.Client;

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

            _channel.ExchangeDeclare("storage", ExchangeType.Topic, durable: true);
        }

        public IModel GetChannel() => _channel;

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

