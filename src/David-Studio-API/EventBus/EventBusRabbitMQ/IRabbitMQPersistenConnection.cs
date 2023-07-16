using RabbitMQ.Client;

namespace EventBusRabbitMQ
{
    public interface IRabbitMQPersistenConnection : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}

