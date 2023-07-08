using System.Text;
using System.Threading.Channels;
using RabbitMQ.Client;

namespace Portfolio.MessageBus.Services
{
    public class StorageClient : IStorageClient
    {
        private const string ExchangeName = "storage";
        private readonly IModel _channel;

        public StorageClient(IModel channel)
        {
            _channel = channel;

            channel.ExchangeDeclare(ExchangeName, ExchangeType.Topic, durable: true);
        }

        public void PublishDeleteImage(string imageUrl)
        {
            string imageName = imageUrl.Substring(imageUrl.LastIndexOf('/') + 1);

            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;

            _channel.BasicPublish(exchange: ExchangeName,
                     routingKey: "images.delete",
                     basicProperties: null,
                     body: Encoding.UTF8.GetBytes(imageName));
        }
    }
}

