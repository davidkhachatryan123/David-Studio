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
    public class Tags : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly ILogger<Tags> _logger;

        public Tags(
            IRepositoryManager repositoryManager,
            IMapper mapper,
            ILogger<Tags> logger)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _logger = logger;
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PageOptions options)
        {
            PageData<Tag>? data = null;

            try
            {
                _logger.LogInformation("Trying to get all tags");

                data = await _repositoryManager.Tags.GetAllAsync(options);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get all tags function thrown exception: {Message}", ex.Message);
            }

            return data is null
                ? NotFound()
                : Ok(new PageData<TagReadDto>
                {
                    Entities = _mapper.Map<IEnumerable<TagReadDto>>(data.Entities),
                    TotalCount = data.TotalCount
                });
        }

        [MapToApiVersion("1.0")]
        [HttpGet("{id}", Name = nameof(GetById))]
        public async Task<IActionResult> GetById(int id)
        {
            Tag? tag = await _repositoryManager.Tags.GetByIdAsync(id);

            _logger.LogInformation("Return tag by id: {Id}", id);

            return tag is null
                ? NotFound()
                : Ok(_mapper.Map<TagReadDto>(tag));
        }

        [MapToApiVersion("1.0")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TagCreateDto tagDto)
        {
            Tag tag = _mapper.Map<Tag>(tagDto);

            Tag? createdTag = await _repositoryManager.Tags.CreateAsync(tag);
            await _repositoryManager.SaveAsync();

            if (createdTag is null) return Conflict();

            _logger.LogInformation("Saved tag with name: {TagName}", tag.Name);

            TagReadDto tagRes = _mapper.Map<TagReadDto>(createdTag);
            return CreatedAtRoute(nameof(GetById), new { id = tagRes.Id }, tagRes);
        }

        [MapToApiVersion("1.0")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TagCreateDto tagDto)
        {
            Tag tag = _mapper.Map<Tag>(tagDto);
            tag.Id = id;

            Tag? updatedTag = await _repositoryManager.Tags.UpdateAsync(tag);
            await _repositoryManager.SaveAsync();

            _logger.LogInformation("Tag was updated by name: {TagName}", tag.Name);

            TagReadDto tagRes = _mapper.Map<TagReadDto>(updatedTag);

            return updatedTag is null
                ? NotFound()
                : CreatedAtRoute(nameof(GetById), new { id = tagRes.Id }, tagRes);
        }

        [MapToApiVersion("1.0")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Tag? tag = await _repositoryManager.Tags.DeleteAsync(id);
            await _repositoryManager.SaveAsync();

            _logger.LogInformation("Delete tag by id: {TagId}", id);

            return tag is null
                ? NotFound()
                : Ok();
        }
    }
}

