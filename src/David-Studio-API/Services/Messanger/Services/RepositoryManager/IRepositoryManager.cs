namespace Messanger.Services.RepositoryManager
{
    public interface IRepositoryManager
    {
        IMessagesRepository Messages { get; }

        Task SaveAsync();
    }
}

