using EventBus.Abstractions;
using Nest;
using Search.IntegrationEvents.Events;
using Search.Services.RepositoryManager;

namespace Search.IntegrationEvents.Handlers
{
    public class RemoveProjectIntegrationEventHandler
        : IIntegrationEventHandler<RemoveProjectIntegrationEvent>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILogger<IndexProjectIntegrationEventHandler> _logger;

        public RemoveProjectIntegrationEventHandler(
            IRepositoryManager repositoryManager,
            ILogger<IndexProjectIntegrationEventHandler> logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        public async Task Handle(RemoveProjectIntegrationEvent data)
        {
            long deleted = await _repositoryManager.Tags.ClearTagsAsync(data.ProjectId);
            var response = await _repositoryManager.Projects.DeleteIndexAsync(data.ProjectId);

            if (response.IsValid)
                _logger.LogInformation("Project {ProjectId} deleted with {TagsCount} tags", data.ProjectId, deleted);
            else
                _logger.LogError("Error occurred when trying to delete project {ProjectId} with {TagsCount} tags", data.ProjectId, deleted);
        }
    }
}
