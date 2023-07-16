using Portfolio.Models;

namespace Portfolio.Services
{
    public interface ITopProjectsRepository
    {
        Task<IEnumerable<Project>> GetAllAsync(int? limit = null);

        Task<int[]> MarkAsync(int[] ids);
        Task<bool> RemoveAsync(int id);

        Task Reorder(int[] projectIds);
    }
}

