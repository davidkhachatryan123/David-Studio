using EventBus.Abstractions;
using Search.IntegrationEvents.Events;
using Search.Services.RepositoryManager;

namespace Search.IntegrationEvents.Handlers
{
    public class UpdateTagIntegrationEventHandler
        : IIntegrationEventHandler<UpdateTagIntegrationEvent>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILogger<UpdateTagIntegrationEventHandler> _logger;

        public UpdateTagIntegrationEventHandler(
            IRepositoryManager repositoryManager,
            ILogger<UpdateTagIntegrationEventHandler> logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        public async Task Handle(UpdateTagIntegrationEvent data)
        {
            var response = await _repositoryManager.Tags.UpdateTagsAsync(data.Tag);

            if (response.IsValid)
                _logger.LogInformation("All tags updated with Id {TagId}", data.Tag.Id);
            else
                _logger.LogError("Error occurred when trying to update all tags with Id { TagId}", data.Tag.Id);
        }
    }
}
