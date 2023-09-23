using EventBus.Abstractions;
using EventBus.Events;
using IdentityServer.Controllers;
using IdentityServer.Dtos;
using IdentityServer.IntegrationEvents.Events;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Services
{
    public class ManageUsersRepository : IManageUsersRepository
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEventBus _eventBus;

        public ManageUsersRepository(
            LinkGenerator linkGenerator,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration,
            ILogger logger,
            UserManager<ApplicationUser> userManager,
            IEventBus eventBus)
        {
            _linkGenerator = linkGenerator;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _logger = logger;
            _userManager = userManager;
            _eventBus = eventBus;
        }

        public async Task SendMfaCodeAsync(string email)
        {
            ApplicationUser user = (await _userManager.FindByEmailAsync(email))!;

            string code = await _userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultEmailProvider);

            _logger.LogInformation("Publishing message to event bus for send email to: {EmailAddress}", user.Email);

            IntegrationEvent @event = new SendTwoFactorCodeEmailIntegrationEvent(user.Email!, code);
            _eventBus.Publish(@event);
        }

        public async Task<string> SendConfirmationEmailAsync(ConfirmEmailRequestDto requestDto)
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(requestDto.UserId);
            if (user is null) return "User not found";

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            string? confirmUrl = _linkGenerator.GetPathByAction(
                _httpContextAccessor.HttpContext!,
                nameof(Account.ConfirmEmail),
                nameof(Account),
                new ConfirmEmailDto
                {
                    UserId = requestDto.UserId,
                    Token = token,
                    ReturnUrl = requestDto.ReturnUrl
                });

            confirmUrl = _configuration["IdentityServer_EmailConfirmation_URL"]! + confirmUrl;

            if (confirmUrl is null)
                return "Error occurred when trying to generate confirmation url";

            _logger.LogInformation("Publishing message to event bus for user email confirmation: {EmailAddress}", user.Email);

            IntegrationEvent @event = new SendConfirmationEmailIntegrationEvent(user.Email!, confirmUrl);
            _eventBus.Publish(@event);

            return "Message sent succesfully";
        }
    }
}
