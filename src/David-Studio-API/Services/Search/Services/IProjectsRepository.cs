using Nest;
using Search.Models;

namespace Search.Services
{
    public interface IProjectsRepository
    {
        Task<IndexResponse> CreateIndexAsync(Project project);
        Task<DeleteResponse> DeleteIndexAsync(int projectId);
    }
}
