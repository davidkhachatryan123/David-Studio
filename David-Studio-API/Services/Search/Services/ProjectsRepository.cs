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

        public async Task<GetResponse<Project>> GetIndexAsync(int projectId)
            => await _client.GetAsync<Project>($"p-{projectId}", s => s
                .Routing(projectId)
            );

        public async Task<IndexResponse> IndexAsync(Project project)
        {
            project.Join = JoinField.Root<Project>();

            var index = await _client.IndexAsync(project, c => c
                .Routing(project.Id)
                .Id(new Id($"p-{project.Id}"))
            );

            return index;
        }

        public async Task<DeleteResponse> DeleteIndexAsync(int projectId)
            => await _client.DeleteAsync<Project>(
                    $"p-{projectId}",
                    c => c.Routing(projectId)
                );
    }
}
