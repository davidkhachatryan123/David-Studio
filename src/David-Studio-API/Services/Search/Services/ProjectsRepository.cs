using Nest;
using Search.Models;
using static System.Net.Mime.MediaTypeNames;
using static Nest.JoinField;

namespace Search.Services
{
    public class ProjectsRepository : IProjectsRepository
    {
        private readonly ElasticClient _client;

        public ProjectsRepository(ElasticClient client)
        {
            _client = client;
        }

        public async Task<Project?> GetIndexByIdAsync(int docId)
        {
            var response = await _client.GetAsync<Project>(docId);

            if (response.IsValid)
                return response.Source;
            else
                return null;
        }

        public async Task<IndexResponse> CreateIndexAsync(Project project)
        {
            project.Join = JoinField.Root<Project>();

            var indexParent = await _client.IndexDocumentAsync(project);

            return indexParent;
        }

        public async Task<UpdateResponse<Project>> UpdateIndexAsync(int docId, Project project)
        {
            var response = await _client.UpdateAsync<Project, Project>(
                docId,
                u => u.Doc(project));

            return response;
        }

        public async Task<DeleteResponse> DeleteIndexAsync(int docId)
        {
            return await _client.DeleteAsync<Project>(docId);
        }
    }
}
