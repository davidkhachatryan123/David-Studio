using Microsoft.AspNetCore.Mvc;
using Portfolio.Dtos;
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

        public TopProjects(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        //[MapToApiVersion("1.0")]
        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{

        //}

        [MapToApiVersion("1.0")]
        [HttpPost]
        public async Task<IActionResult> Mark([FromBody] IEnumerable<ProjectReadDto> projectDto)
        {
            int[] projectIds = projectDto.Select(p => p.Id).ToArray();
            int[] addedProjectIds;

            try
            {
                addedProjectIds = await _repositoryManager.TopProjects.MarkAsTop(projectIds);
                await _repositoryManager.SaveAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(addedProjectIds);
        }

        //[MapToApiVersion("1.0")]
        //[HttpDelete]
        //[Route("{id}")]
        //public async Task<IActionResult> Remove(int id)
        //{

        //}

        //[MapToApiVersion("1.0")]
        //[HttpPost]
        //public async Task<IActionResult> Reorder([FromBody] IEnumerable<ProjectReadDto> projectDto)
        //{

        //}
    }
}
