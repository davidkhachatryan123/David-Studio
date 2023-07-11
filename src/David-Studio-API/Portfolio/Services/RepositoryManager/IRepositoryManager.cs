using Portfolio.Models;

namespace Portfolio.Services
{
    public interface IRepositoryManager
    {
        IProjectsRepository Projects { get; }
        ITagsRepository Tags { get; }

        ITopProjectsRepository TopProjects { get; }

        Task SaveAsync();
    }
}

