using EventBus.Abstractions;
using Google.Protobuf;
using Storage.Services;

namespace Storage.IntegrationEvents
{
    public class DeleteImagesIntegrationEventHandler
        : IIntegrationEventHandler<string>
    {
        private readonly ILogger<DeleteImagesIntegrationEventHandler> _logger;
        private readonly IRepositoryManager _repositoryManager;

        public DeleteImagesIntegrationEventHandler(
            IRepositoryManager repositoryManager,
            ILogger<DeleteImagesIntegrationEventHandler> logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        public async Task Handle(string data)
        {
            _logger.LogInformation("Handling request to delete image: {Name}", data);

            string uniqueNameOfImage = data.Substring(data.LastIndexOf('/') + 1);

            await _repositoryManager.Images.DeleteAsync(uniqueNameOfImage);
            await _repositoryManager.SaveAsync();
        }
    }
}
