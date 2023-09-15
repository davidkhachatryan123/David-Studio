using Search.Models;
using Services.Common.Models;

namespace Search.Services
{
    public interface IProjectsRepository
    {
        Task<Project?> GetIndexByIdAsync(int id);
        Task<Project> CreateIndexAsync(Project project);
        Task<Project> UpdateIndexAsync(Project project);
        Task<Project> DeleteIndexAsync(int id);
    }
}
