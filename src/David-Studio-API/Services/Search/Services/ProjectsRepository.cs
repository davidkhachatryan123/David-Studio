using Nest;
using Search.Models;

namespace Search.Services
{
    public class ProjectsRepository : IProjectsRepository
    {
        private readonly ElasticClient _client;

        public ProjectsRepository(ElasticClient client)
        {
            _client = client;
        }

        public Task<Project?> GetIndexByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Project> CreateIndexAsync(Project project)
        {
            throw new NotImplementedException();
        }

        public Task<Project> UpdateIndexAsync(Project project)
        {
            throw new NotImplementedException();
        }

        public Task<Project> DeleteIndexAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
