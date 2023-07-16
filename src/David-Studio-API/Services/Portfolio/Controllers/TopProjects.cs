using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Dtos;
using Portfolio.Models;
using Portfolio.Services;
using Services.Common.Models;

namespace Portfolio.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TopProjects : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly ILogger<TopProjects> _logger;

        public TopProjects(
            IRepositoryManager repositoryManager,
            IMapper mapper,
            ILogger<TopProjects> logger)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _logger = logger;
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        public async Task<IActionResult> GetAll(int? Limit = null)
        {
            IEnumerable<Project> projects =
                await _repositoryManager.TopProjects.GetAllAsync(Limit);

            IEnumerable<ProjectReadDto> projectsResult =
                _mapper.Map<IEnumerable<ProjectReadDto>>(projects);

            _logger.LogInformation("Get all projects marked as Top by limit: {Limit}", Limit);

            return Ok(projectsResult);
        }

        [MapToApiVersion("1.0")]
        [HttpPost]
        public async Task<IActionResult> Mark([FromBody] IEnumerable<ProjectReadDto> projectDto)
        {
            int[] projectIds = projectDto.Select(p => p.Id).ToArray();
            int[] addedProjectIds;

            try
            {
                _logger.LogInformation("Trying to mark as Top projects by ids: {@Ids}", projectIds);

                addedProjectIds = await _repositoryManager.TopProjects.MarkAsync(projectIds);
                await _repositoryManager.SaveAsync();

                _logger.LogInformation("Marked as Top projects by ids: {@Ids}", addedProjectIds);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error was occurred while trying to mark projects as Top: {ExceptionMessage}", ex.Message);

                return BadRequest(ex.Message);
            }

            return addedProjectIds.Length == 0
                ? NotFound()
                : Ok(addedProjectIds);
        }

        [MapToApiVersion("1.0")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            bool res = await _repositoryManager.TopProjects.RemoveAsync(id);
            await _repositoryManager.SaveAsync();

            _logger.LogInformation("Remove project from top by id: {Id}", id);

            return !res ? NotFound() : Ok();
        }

        [MapToApiVersion("1.0")]
        [HttpPost]
        [Route("Reorder")]
        public async Task<IActionResult> Reorder([FromBody] int[] projectIds)
        {
            try
            {
                await _repositoryManager.TopProjects.Reorder(projectIds);

                _logger.LogInformation("Projects reordering with fllowing order: {@Ids}", projectIds);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error was occurred while trying to reorder projects as Top: {ExceptionMessage}", ex.Message);

                return BadRequest(ex.Message);
            }

            await _repositoryManager.SaveAsync();

            return Ok();
        }
    }
}
