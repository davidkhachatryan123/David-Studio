using AutoMapper;
using Azure;
using EventBus.Abstractions;
using EventBus.Events;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Dtos;
using Portfolio.Grpc;
using Portfolio.IntegrationEvents.Events;
using Portfolio.Models;
using Portfolio.Services;
using Services.Common.Models;

namespace Portfolio.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class Projects : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IStorageDataClient _storageData;
        private readonly IEventBus _eventBus;
        private readonly IMapper _mapper;
        private readonly ILogger<Projects> _logger;

        public Projects(
            IRepositoryManager repositoryManager,
            IStorageDataClient storageData,
            IEventBus eventBus,
            IMapper mapper,
            ILogger<Projects> logger)
        {
            _repositoryManager = repositoryManager;
            _storageData = storageData;
            _eventBus = eventBus;
            _mapper = mapper;
            _logger = logger;
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PageOptions options)
        {
            PageData<Project>? data = null;

            try
            {
                _logger.LogInformation("Trying to get all projects");

                data = await _repositoryManager.Projects.GetAllAsync(options);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get all projects function thrown exception: {Message}", ex.Message);
            }

            return data is null
                ? NotFound()
                : Ok(new PageData<ProjectReadDto>
                {
                    Entities = _mapper.Map<IEnumerable<ProjectReadDto>>(data.Entities),
                    TotalCount = data.TotalCount
                });
        }

        [MapToApiVersion("1.0")]
        [HttpGet("{id}", Name = nameof(Projects) + nameof(GetById))]
        public async Task<IActionResult> GetById(int id)
        {
            Project? project = await _repositoryManager.Projects.GetByIdAsync(id);

            _logger.LogInformation("Return project by id: {Id}", id);

            return project is null
                ? NotFound()
                : Ok(_mapper.Map<ProjectReadDto>(project));
        }

        [MapToApiVersion("1.0")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Create([FromForm] ProjectCreateDto projectDto)
        {
            _logger.LogInformation("Saving image in storage by name: {FileName}", projectDto.File.FileName);
            ImageReadDto image = await _storageData.StoreImageAsync(projectDto.File);

            Project? project = _mapper.Map<Project>(projectDto);
            project.ImageUrl = image.ImageUrl;

            project = await _repositoryManager.Projects.CreateAsync(project);
            await _repositoryManager.SaveAsync();

            _logger.LogInformation("Save project with name: {ProjectName}", project.Name);

            ProjectReadDto projectRes = _mapper.Map<ProjectReadDto>(project);
            return CreatedAtRoute(nameof(Projects) + nameof(GetById), new { id = projectRes.Id }, projectRes);
        }

        [MapToApiVersion("1.0")]
        [HttpPut("{id}"), DisableRequestSizeLimit]
        public async Task<IActionResult> Update(int id, [FromForm] ProjectCreateDto projectDto)
        {
            ImageReadDto? image = null;

            if (projectDto.File is not null)
            {
                _logger.LogInformation("Saving image in storage by name: {FileName}", projectDto.File.FileName);
                image = await _storageData.StoreImageAsync(projectDto.File);
            }

            Project? project = _mapper.Map<Project>(projectDto);
            project.Id = id;

            project = await _repositoryManager.Projects.UpdateAsync(project);

            if (projectDto.File is not null)
            {
                _logger.LogInformation("Publishing message to event bus for remove old image by url: {ImageUrl}", project.ImageUrl);
                IntegrationEvent @event = new ImagesDeleteIntegrationEvent(project.ImageUrl);
                _eventBus.Publish(@event);
            }

            if (image is not null)
                project.ImageUrl = image.ImageUrl;

            await _repositoryManager.SaveAsync();

            _logger.LogInformation("Project was updated by name: {ProjectName}", project.Name);

            ProjectReadDto projectRes = _mapper.Map<ProjectReadDto>(project);

            return CreatedAtRoute(nameof(GetById), new { id = projectRes.Id }, projectRes);
        }

        [MapToApiVersion("1.0")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Project? project = await _repositoryManager.Projects.DeleteAsync(id);
            await _repositoryManager.SaveAsync();

            if (project is null) return NotFound();

            _logger.LogInformation("Deleted project by id: {ProjectId}", project.Id);

            _logger.LogInformation("Publishing message to event bus for remove image by url: {ImageUrl}", project.ImageUrl);
            IntegrationEvent @event = new ImagesDeleteIntegrationEvent(project.ImageUrl);
            _eventBus.Publish(@event);

            return Ok();
        }
    }
}
