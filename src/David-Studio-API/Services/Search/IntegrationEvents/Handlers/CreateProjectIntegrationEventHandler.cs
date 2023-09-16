using EventBus.Abstractions;
using Search.IntegrationEvents.Events;
using Search.Services.RepositoryManager;

namespace Search.IntegrationEvents.Handlers
{
    public class CreateProjectIntegrationEventHandler
        : IIntegrationEventHandler<CreateProjectIntegrationEvent>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILogger<CreateProjectIntegrationEventHandler> _logger;

        public CreateProjectIntegrationEventHandler(
            IRepositoryManager repositoryManager,
            ILogger<CreateProjectIntegrationEventHandler> logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        public async Task Handle(CreateProjectIntegrationEvent data)
        {
            var response =
                await _repositoryManager.Projects.CreateIndexAsync(data.Project);

            if (response.IsValid)
                _logger.LogInformation("Index Project document with ID {ProjectId} succeeded.", data.Project.Id);
        }
    }
}
