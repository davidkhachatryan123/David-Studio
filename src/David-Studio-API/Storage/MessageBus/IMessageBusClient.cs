using RabbitMQ.Client;

namespace Storage.MessageBus
{
    public interface IMessageBusClient
    {
        IModel GetChannel();
    }
}

