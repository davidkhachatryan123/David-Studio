using AutoMapper;
using Messanger.Dtos;
using Messanger.Models;
using Messanger.Services.RepositoryManager;
using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        public async Task<IActionResult> NewMessage([FromBody] ContactFormDto contactFromDto)
        {
            Message message = _mapper.Map<Message>(contactFromDto);

            _logger.LogInformation("Send new message from: {FullName} - {Email}", contactFromDto.FullName, contactFromDto.Email);

            await _repositoryManager.Messages.NewMessageAsync(message);
            await _repositoryManager.SaveAsync();

            return Ok("You sent message successfully. Please wait for answer");
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        [Route(nameof(GetMessagesList))]
        public async Task<IActionResult> GetMessagesList([FromQuery] PageOptions options)
        {
            PageData<Message>? messagesList = null;

            try
            {
                _logger.LogInformation("Trying to get user messages with pagination");

                messagesList = await _repositoryManager.Messages.GetMessagesListAsync(options);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error was occurred when trying to get list of the messages: {Message}", ex.Message);
            }

            return messagesList is null
                ? NotFound()
                : Ok(new PageData<MessagesListItemDto>
                {
                    Entities = _mapper.Map<IEnumerable<MessagesListItemDto>>(messagesList.Entities),
                    TotalCount = messagesList.TotalCount
                });
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        [Route(nameof(ReadMessage))]
        public async Task<IActionResult> ReadMessage(int id)
        {
            Message? message = await _repositoryManager.Messages.ReadMessageAsync(id);

            return message is null
                ? NotFound()
                : Ok(_mapper.Map<MessageReadDto>(message));
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        [Route(nameof(ReadAnswer))]
        public async Task<IActionResult> ReadAnswer(int id)
        {
            Answer? answer = await _repositoryManager.Messages.ReadAnswerAsync(id);

            return answer is null
                ? NotFound("This message has no answer")
                : Ok(_mapper.Map<AnswerReadDto>(answer));
        }

        [MapToApiVersion("1.0")]
        [HttpPost]
        [Route(nameof(Answer))]
        public async Task<IActionResult> Answer([FromQuery] int id, [FromBody] string message)
        {
            Answer? answer = null;

            try
            {
                answer = await _repositoryManager.Messages.AnswerAsync(id, message);
                await _repositoryManager.SaveAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return Ok(_mapper.Map<AnswerReadDto>(answer));
        }
    }
}
