using AutoMapper;
using EventBus.Abstractions;
using EventBus.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Dtos;
using Portfolio.IntegrationEvents.Events;
using Portfolio.Models;
using Portfolio.Services;

namespace Portfolio.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TopProjects : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IEventBus _eventBus;
        private readonly IMapper _mapper;
        private readonly ILogger<TopProjects> _logger;

        public TopProjects(
            IRepositoryManager repositoryManager,
            IEventBus eventBus,
            IMapper mapper,
            ILogger<TopProjects> logger)
        {
            _repositoryManager = repositoryManager;
            _eventBus = eventBus;
            _mapper = mapper;
            _logger = logger;
        }

        [AllowAnonymous]
        [MapToApiVersion("1.0")]
        [HttpGet]
        public async Task<IActionResult> GetAll(int? Limit = null)
        {
            IEnumerable<Project> projects =
                await _repositoryManager.TopProjects.GetAllAsync(Limit);

            IEnumerable<ProjectReadDto> projectsResult =
                _mapper.Map<IEnumerable<ProjectReadDto>>(projects);

            _logger.LogInformation("Get all Top projects by limit: {Limit}", Limit);

            return Ok(projectsResult);
        }

        [MapToApiVersion("1.0")]
        [HttpPost]
        public async Task<IActionResult> Mark([FromBody] IEnumerable<ProjectReadDto> projectDto)
        {
            int[] projectIds = projectDto.Select(p => p.Id).ToArray();
            IEnumerable<TopProject> markedTopProjects;

            try
            {
                markedTopProjects = await _repositoryManager.TopProjects.MarkAsync(projectIds);
                await _repositoryManager.SaveAsync();

                IEnumerable<Project> projects = await _repositoryManager.TopProjects.GetAllAsync();

                _logger.LogInformation("Publishing message to event bus -> To add rank to projects for search engine");

                IntegrationEvent @event = new IndexTopProjectsIntegrationEvent(
                    _mapper.Map<IEnumerable<TopProjectDto>>(
                        projects.Select(p => p.TopProject)
                    )
                );

                _eventBus.Publish(@event);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error occurred while trying to mark projects as Top: {ExceptionMessage}", ex.Message);

                return BadRequest(ex.Message);
            }

            return !markedTopProjects.Any()
                ? NotFound()
                : Ok(markedTopProjects);
        }

        [MapToApiVersion("1.0")]
        [HttpPost]
        [Route("Reorder")]
        public async Task<IActionResult> Reorder([FromBody] int[] projectIds)
        {
            try
            {
                IEnumerable<TopProject> topProjects = await _repositoryManager.TopProjects.Reorder(projectIds);

                _logger.LogInformation("Publishing message to event bus -> To reorder projects ranks for search engine");

                IntegrationEvent @event = new IndexTopProjectsIntegrationEvent(
                    _mapper.Map<IEnumerable<TopProjectDto>>(topProjects));

                _eventBus.Publish(@event);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error was occurred while trying to reorder projects as Top: {ExceptionMessage}", ex.Message);

                return BadRequest(ex.Message);
            }

            await _repositoryManager.SaveAsync();

            return Ok();
        }

        [MapToApiVersion("1.0")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            bool res = await _repositoryManager.TopProjects.RemoveAsync(id);
            await _repositoryManager.SaveAsync();

            if (!res)
                return NotFound();

            _logger.LogInformation("Removed project from top by id: {ProjectId}", id);

            IEnumerable<Project> projects = await _repositoryManager.TopProjects.GetAllAsync();

            _logger.LogInformation("Publishing message to event bus -> To remove one of project and reorder another projects ranks for search engine");

            IntegrationEvent @eventReorder = new IndexTopProjectsIntegrationEvent(
                id,
                _mapper.Map<IEnumerable<TopProjectDto>>(
                    projects.Select(p => p.TopProject)
                )
            );

            _eventBus.Publish(@eventReorder);

            return Ok();
        }
    }
}
