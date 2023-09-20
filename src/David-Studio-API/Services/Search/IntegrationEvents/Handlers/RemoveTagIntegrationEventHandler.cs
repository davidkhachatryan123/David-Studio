using EventBus.Abstractions;
using Search.IntegrationEvents.Events;
using Search.Services.RepositoryManager;

namespace Search.IntegrationEvents.Handlers
{
    public class RemoveTagIntegrationEventHandler
    : IIntegrationEventHandler<RemoveTagIntegrationEvent>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILogger<RemoveTagIntegrationEventHandler> _logger;

        public RemoveTagIntegrationEventHandler(
            IRepositoryManager repositoryManager,
            ILogger<RemoveTagIntegrationEventHandler> logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        public async Task Handle(RemoveTagIntegrationEvent data)
        {
            var deleted = await _repositoryManager.Tags.DeleteTagsAsync(data.TagId);

            _logger.LogInformation("Deleted {TagsCount} tags with Id {TagId}", deleted, data.TagId);
        }
    }
}
