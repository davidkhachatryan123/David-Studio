namespace Search.Services.RepositoryManager
{
    public interface IRepositoryManager
    {
        IProjectsRepository Projects { get; }
        ITagsRepository Tags { get; }
    }
}

