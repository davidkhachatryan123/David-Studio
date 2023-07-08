using Portfolio.MessageBus.Services;
using Portfolio.Models;
using Portfolio.Services;
using RabbitMQ.Client;

namespace Portfolio.MessageBus
{
    public class MessageBusClient : IMessageBusClient
    {
        private IConnection _connection;
        private IModel _channel;

        private StorageClient _storageClient = null!;

        public MessageBusClient(IConfiguration configuration)
        {
            ConnectionFactory factory = new()
            {
                Uri = new Uri(configuration.GetConnectionString("EventBus")!)
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public StorageClient StorageClient
        {
            get
            {
                _storageClient ??= new StorageClient(_channel);

                return _storageClient;
            }
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

