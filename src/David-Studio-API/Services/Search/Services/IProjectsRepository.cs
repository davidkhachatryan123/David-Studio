using Nest;
using Search.Models;
using Services.Common.Models;

namespace Search.Services
{
    public interface IProjectsRepository
    {
        Task<Project?> GetIndexByIdAsync(int docId);
        Task<IndexResponse> CreateIndexAsync(Project project);
        Task<UpdateResponse<Project>> UpdateIndexAsync(int docId, Project project);
        Task<DeleteResponse> DeleteIndexAsync(int docId);
    }
}
