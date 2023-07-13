using RabbitMQ.Client;

namespace Services.Common.EventBus
{
    public interface IMessageBusClient : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}

