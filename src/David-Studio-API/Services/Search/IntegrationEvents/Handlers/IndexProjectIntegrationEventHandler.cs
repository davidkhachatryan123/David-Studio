using AutoMapper;
using EventBus.Abstractions;
using Nest;
using Search.IntegrationEvents.Events;
using Search.Models;
using Search.Services.RepositoryManager;

namespace Search.IntegrationEvents.Handlers
{
    public class IndexProjectIntegrationEventHandler
        : IIntegrationEventHandler<IndexProjectIntegrationEvent>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly ILogger<IndexProjectIntegrationEventHandler> _logger;

        public IndexProjectIntegrationEventHandler(
            IRepositoryManager repositoryManager,
            IMapper mapper,
            ILogger<IndexProjectIntegrationEventHandler> logger)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(IndexProjectIntegrationEvent data)
        {
            IndexResponse response =
                await _repositoryManager.Projects.CreateIndexAsync(
                    _mapper.Map<Project>(data.Project));

            if (response.IsValid)
                _logger.LogInformation("Index Project {ProjectId}: succeeded", data.Project.Id);
            else
                _logger.LogError("Index Project {ProjectId}: was unsuccessful", data.Project.Id);


            long deleted = await _repositoryManager.Tags.ClearTagsAsync(data.Project.Id);
            _logger.LogInformation("Count of deleted Tags: {DeletedCount}", deleted);


            BulkResponse tagsResponse =
                await _repositoryManager.Tags.IndexRangeAsync(data.Project.Tags, data.Project.Id);

            if (tagsResponse.IsValid)
                _logger.LogInformation("Index Tags for Project {ProjectId}: succeeded", data.Project.Id);
            else
                _logger.LogError("Index Tags for Project {ProjectId}: was unsuccessful", data.Project.Id);
        }
    }
}
