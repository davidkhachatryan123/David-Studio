namespace Storage.Services
{
    public interface IRepositoryManager
    {
        IFileManagement Files { get; }

        Task SaveAsync();
    }
}

