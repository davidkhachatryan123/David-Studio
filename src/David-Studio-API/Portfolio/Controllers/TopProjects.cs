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

        public TopProjects(
            IRepositoryManager repositoryManager,
            IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        public async Task<IActionResult> GetAll(int? Limit = null)
        {
            IEnumerable<Project> projects =
                await _repositoryManager.TopProjects.GetAllAsync(Limit);

            IEnumerable<ProjectReadDto> projectsResult =
                _mapper.Map<IEnumerable<ProjectReadDto>>(projects);

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
                addedProjectIds = await _repositoryManager.TopProjects.MarkAsync(projectIds);
                await _repositoryManager.SaveAsync();
            }
            catch (Exception ex)
            {
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

            return !res ? NotFound() : Ok();
        }

        //[MapToApiVersion("1.0")]
        //[HttpPost]
        //public async Task<IActionResult> Reorder([FromBody] IEnumerable<ProjectReadDto> projectDto)
        //{

        //}
    }
}
