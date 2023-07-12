using RabbitMQ.Client;

namespace Storage.MessageBus
{
    public interface IMessageBusClient
    {
        void CreateSubsciber(string eventSource, Func<string, string, Task> eventExecuter);
    }
}

