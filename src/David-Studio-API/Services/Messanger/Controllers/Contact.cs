using AutoMapper;
using Messanger.Dtos;
using Messanger.Models;
using Messanger.Services.RepositoryManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Common.Models;

namespace Messanger.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class Contact : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly ILogger<Contact> _logger;

        public Contact(
            IRepositoryManager repositoryManager,
            IMapper mapper,
            ILogger<Contact> logger)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _logger = logger;
        }

        [MapToApiVersion("1.0")]
        [HttpPost]
        public async Task<IActionResult> NewMessage([FromBody] ContactFormDto contactFromDto)
        {
            Message message = _mapper.Map<Message>(contactFromDto);

            _logger.LogInformation("Send new message from: {FullName} - {Email}", contactFromDto.FullName, contactFromDto.Email);

            await _repositoryManager.Messages.NewMessageAsync(message);
            await _repositoryManager.SaveAsync();

            return Ok("You sent message successfully. Please wait for answer");
        }
    }
}