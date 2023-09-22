using EventBus.Abstractions;
using Google.Protobuf;
using Storage.IntegrationEvents.Events;
using Storage.Services;

namespace Storage.IntegrationEvents.Handlers
{
    public class ImagesDeleteIntegrationEventHandler
        : IIntegrationEventHandler<ImagesDeleteIntegrationEvent>
    {
        private readonly ILogger<ImagesDeleteIntegrationEventHandler> _logger;
        private readonly IRepositoryManager _repositoryManager;

        public ImagesDeleteIntegrationEventHandler(
            IRepositoryManager repositoryManager,
            ILogger<ImagesDeleteIntegrationEventHandler> logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        public async Task Handle(ImagesDeleteIntegrationEvent @event)
        {
            _logger.LogInformation("Handling request to delete image: {ImageUrl}", @event.ImageUrl);

            string uniqueNameOfImage =
                @event.ImageUrl.Substring(@event.ImageUrl.LastIndexOf('/') + 1);

            await _repositoryManager.Images.DeleteAsync(uniqueNameOfImage);
            await _repositoryManager.SaveAsync();
        }
    }
}
