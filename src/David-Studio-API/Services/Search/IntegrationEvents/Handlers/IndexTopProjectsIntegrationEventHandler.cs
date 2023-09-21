using EventBus.Abstractions;
using Nest;
using Search.IntegrationEvents.Events;
using Search.Models;
using Search.Services.RepositoryManager;

namespace Search.IntegrationEvents.Handlers
{
    public class IndexTopProjectsIntegrationEventHandler
    : IIntegrationEventHandler<IndexTopProjectsIntegrationEvent>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILogger<IndexTopProjectsIntegrationEventHandler> _logger;

        public IndexTopProjectsIntegrationEventHandler(
            IRepositoryManager repositoryManager,
            ILogger<IndexTopProjectsIntegrationEventHandler> logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        public async Task Handle(IndexTopProjectsIntegrationEvent data)
        {
            if (data.RemovedTopProjectId is not null)
            {
                GetResponse<Project> project =
                    await _repositoryManager.Projects.GetIndexAsync((int)data.RemovedTopProjectId);

                project.Source.Rank = 0;

                IndexResponse response =
                    await _repositoryManager.Projects.IndexAsync(project.Source);

                if (response.IsValid)
                    _logger.LogInformation("Change rank of project {ProjectId} to {Rank}", data.RemovedTopProjectId, project.Source.Rank);
                else
                    _logger.LogError("Error occurred when trying to update rank of project {ProjectId}", data.RemovedTopProjectId);
            }

            foreach (var topProject in data.TopProjects)
            {
                GetResponse<Project> project =
                    await _repositoryManager.Projects.GetIndexAsync(topProject.ProjectId);

                project.Source.Rank = topProject.Rank;

                IndexResponse response =
                    await _repositoryManager.Projects.IndexAsync(project.Source);

                if (response.IsValid)
                    _logger.LogInformation("Change rank of project {ProjectId} to {Rank}", topProject.ProjectId, topProject.Rank);
                else
                    _logger.LogError("Error occurred when trying to update rank of project {ProjectId}", topProject.ProjectId);
            }
        }
    }
}
