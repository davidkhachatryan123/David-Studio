namespace Portfolio.Services
{
    public interface IRepositoryManager
    {
        ITagsRepository Tags { get; }

        Task SaveAsync();
    }
}

