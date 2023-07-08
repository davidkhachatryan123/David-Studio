using Portfolio.MessageBus.Services;

namespace Portfolio.MessageBus
{
    public interface IMessageBusClient
    {
        StorageClient StorageClient { get; }
    }
}

