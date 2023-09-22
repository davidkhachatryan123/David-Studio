using Portfolio.Models;
using Services.Common.Models;

namespace Portfolio.Services
{
    public interface IProjectsRepository
    {
        Task<PageData<Project>> GetAllAsync(PageOptions options);
        Task<Project?> GetByIdAsync(int id);
        Task<Project> CreateAsync(Project project);
        Task<Project> UpdateAsync(Project project);
        Task<Project?> DeleteAsync(int id);
    }
}

