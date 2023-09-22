using Nest;
using Search.Models;

namespace Search.Services
{
    public interface IProjectsRepository
    {
        Task<GetResponse<Project>> GetIndexAsync(int projectId);
        Task<IndexResponse> IndexAsync(Project project);
        Task<DeleteResponse> DeleteIndexAsync(int projectId);
    }
}
