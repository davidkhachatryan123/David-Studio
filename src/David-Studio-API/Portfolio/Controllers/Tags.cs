using System;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Dtos;
using Portfolio.Models;
using Portfolio.Services;

namespace Portfolio.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class Tags : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public Tags(
            IRepositoryManager repositoryManager,
            IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] TableOptionsDto options)
        {
            TablesDataDto<Tag> data = await _repositoryManager.Tags.GetAllAsync(options);

            return Ok(new TablesDataDto<TagReadDto>
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

            TagReadDto tagRes = _mapper.Map<TagReadDto>(updatedTag);

            return updatedTag is null
                ? NotFound()
                : CreatedAtRoute(nameof(GetById), new { id = tagRes.Id }, tagRes);
        }

        [MapToApiVersion("1.0")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleteId = await _repositoryManager.Tags.DeleteAsync(id);
            await _repositoryManager.SaveAsync();

            return deleteId
                ? BadRequest()
                : Ok();
        }
    }
}

