using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Services.Common.EventBus
{
    public class MessageBus : IMessageBus
    {
        private readonly IMessageBusClient _messageBusClient;
        private readonly string _exchangeName;

        public MessageBus(IMessageBusClient messageBusClient, string ExchangeName)
        {
            _messageBusClient = messageBusClient
                ?? throw new ArgumentNullException(nameof(messageBusClient));

            _exchangeName = ExchangeName;
        }

        public void Publish<TEventSource, TEventAction, TData>
            (TEventSource source, TEventAction action, TData data)
            where TData : class
            where TEventSource : Enum
            where TEventAction : Enum
        {
            if (!_messageBusClient.IsConnected)
                _messageBusClient.TryConnect();

            byte[] body = JsonSerializer.SerializeToUtf8Bytes(data);
            string eventSource = Enum.GetName(typeof(TEventSource), source)!.ToLower();
            string eventAction = Enum.GetName(typeof(TEventAction), action)!.ToLower();

            using var channel = _messageBusClient.CreateModel();
            channel.ExchangeDeclare(_exchangeName, ExchangeType.Topic, durable: true);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: _exchangeName,
                     routingKey: $"{eventSource}.{eventAction}",
                     basicProperties: null,
                     body: body);
        }
    }
}
