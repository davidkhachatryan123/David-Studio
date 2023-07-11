using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Dtos;
using Portfolio.Grpc;
using Portfolio.MessageBus;
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
        private readonly IMessageBusClient _messageBus;
        private readonly IMapper _mapper;
        private readonly ILogger<Projects> _logger;

        public Projects(
            IRepositoryManager repositoryManager,
            IStorageDataClient storageData,
            IMessageBusClient messageBus,
            IMapper mapper,
            ILogger<Projects> logger)
        {
            _repositoryManager = repositoryManager;
            _storageData = storageData;
            _messageBus = messageBus;
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

        //[MapToApiVersion("1.0")]
        //[HttpGet]
        //public async Task<IActionResult> GetAllWithTop([FromQuery] PageOptions options)
        //{

        //}

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
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Create([FromForm] ProjectCreateDto projectDto)
        {
            ImageReadDto image = await _storageData.StoreImageAsync(projectDto.File);

            Project? project = _mapper.Map<Project>(projectDto);
            project.ImageUrl = image.ImageUrl;

            project = await _repositoryManager.Projects.CreateAsync(project);
            await _repositoryManager.SaveAsync();

            ProjectReadDto projectRes = _mapper.Map<ProjectReadDto>(project);
            return CreatedAtRoute(nameof(Projects) + nameof(GetById), new { id = projectRes.Id }, projectRes);
        }

        [MapToApiVersion("1.0")]
        [HttpPut("{id}"), DisableRequestSizeLimit]
        public async Task<IActionResult> Update(int id, [FromForm] ProjectCreateDto projectDto)
        {
            ImageReadDto image = await _storageData.StoreImageAsync(projectDto.File);

            Project? project = _mapper.Map<Project>(projectDto);
            project.Id = id;

            project = await _repositoryManager.Projects.UpdateAsync(project);

            _messageBus.StorageClient.PublishDeleteImage(project.ImageUrl);
            project.ImageUrl = image.ImageUrl;

            await _repositoryManager.SaveAsync();

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

            _messageBus.StorageClient.PublishDeleteImage(project.ImageUrl);

            return Ok();
        }
    }
}
