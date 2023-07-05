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
    public class Projects : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly ILogger<Projects> _logger;

        public Projects(
            IRepositoryManager repositoryManager,
            IMapper mapper,
            ILogger<Projects> logger)
        {
            _repositoryManager = repositoryManager;
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
                data = await _repositoryManager.Projects.GetAllAsync(options);
            }
            catch (Exception ex) { _logger.LogError(ex.Message); }

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

            return project is null
                ? NotFound()
                : Ok(_mapper.Map<ProjectReadDto>(project));
        }

        [MapToApiVersion("1.0")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProjectCreateDto projectDto)
        {
            Project? project = _mapper.Map<Project>(projectDto);

            project = await _repositoryManager.Projects.CreateAsync(project);
            await _repositoryManager.SaveAsync();

            ProjectReadDto projectRes = _mapper.Map<ProjectReadDto>(project);
            return CreatedAtRoute(nameof(Projects) + nameof(GetById), new { id = projectRes.Id }, projectRes);
        }

        [MapToApiVersion("1.0")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProjectCreateDto projectDto)
        {
            Project project = _mapper.Map<Project>(projectDto);
            project.Id = id;

            Project? updatedProject = await _repositoryManager.Projects.UpdateAsync(project);
            await _repositoryManager.SaveAsync();

            ProjectReadDto projectRes = _mapper.Map<ProjectReadDto>(updatedProject);

            return updatedProject is null
                ? NotFound()
                : CreatedAtRoute(nameof(GetById), new { id = projectRes.Id }, projectRes);
        }

        [MapToApiVersion("1.0")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleteId = await _repositoryManager.Projects.DeleteAsync(id);
            await _repositoryManager.SaveAsync();

            return !deleteId
                ? NotFound()
                : Ok();
        }
    }
}

