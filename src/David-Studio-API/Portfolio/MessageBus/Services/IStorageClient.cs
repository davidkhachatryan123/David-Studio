namespace Portfolio.MessageBus.Services
{
    public interface IStorageClient
    {
        void PublishDeleteImage(string imageUrl);
    }
}

