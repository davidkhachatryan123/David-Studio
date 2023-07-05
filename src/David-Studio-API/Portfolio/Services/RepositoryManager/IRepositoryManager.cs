using Portfolio.Models;

namespace Portfolio.Services
{
    public interface IRepositoryManager
    {
        IBaseRepository<Tag> Tags { get; }
        IBaseRepository<Project> Projects { get; }

        Task SaveAsync();
    }
}

