using Azure;
using EventBus.Abstractions;
using EventBus.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Users.Dtos;
using Users.Grpc.Clients;
using Users.IntegrationEvents.Events;

namespace Users.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class Manage : ControllerBase
    {
        private readonly IEventBus _eventBus;
        private readonly IManageUsersClient _manageUsersClient;
        private readonly ILogger<Manage> _logger;

        public Manage(
            IEventBus eventBus,
            IManageUsersClient manageUsersClient,
            ILogger<Manage> logger)
        {
            _eventBus = eventBus;
            _manageUsersClient = manageUsersClient;
            _logger = logger;
        }

        [MapToApiVersion("1.0")]
        [HttpPost]
        [Route(nameof(SendConfirmationEmail))]
        public async Task<IActionResult> SendConfirmationEmail([FromBody] ConfirmEmailRequestDto requestDto)
        {
            _logger.LogInformation("Trying to get email confirmation token for user: {UserId}", requestDto.UserId);

            TokenResponse response = await _manageUsersClient.GetEmailConfirmationTokenAsync(requestDto.UserId);

            string? confirmUrl = Url.Action(
                nameof(ConfirmEmail),
                nameof(Manage),
                new ConfirmEmailDto
                {
                    UserId = requestDto.UserId,
                    Token = response.Token,
                    ReturnUrl = requestDto.ReturnUrl
                },
                Request.Scheme);

            if (confirmUrl is null)
                return BadRequest("Confirm email action url is null");

            _logger.LogInformation("Publishing message to event bus for send confirmation email to: {EmailAddress}", response.Email);

            IntegrationEvent @event = new SendConfirmationEmailIntegrationEvent(response.Email, confirmUrl);
            _eventBus.Publish(@event);

            return Ok();
        }

        [AllowAnonymous]
        [MapToApiVersion("1.0")]
        [HttpGet]
        [Route(nameof(ConfirmEmail))]
        public IActionResult ConfirmEmail([FromQuery] ConfirmEmailDto confirmEmailDto)
        {
            IntegrationEvent @event = new EmailConfirmationRequestIntegrationEvent(confirmEmailDto.UserId, confirmEmailDto.Token);
            _eventBus.Publish(@event);

            return confirmEmailDto.ReturnUrl is null
                ? Ok("Email is confirmed")
                : Redirect(confirmEmailDto.ReturnUrl);
        }
    }
}
