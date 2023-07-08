namespace Storage.Services
{
    public interface IRepositoryManager
    {
        IImagesService Images { get; }

        Task SaveAsync();
    }
}

