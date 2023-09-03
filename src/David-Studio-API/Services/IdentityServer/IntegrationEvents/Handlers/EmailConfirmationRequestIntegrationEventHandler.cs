using EventBus.Abstractions;
using Google.Protobuf;
using IdentityServer.IntegrationEvents.Events;
using IdentityServer.Models;
using IdentityServer.RepositoryManager.Services;
using Microsoft.AspNetCore.Identity;

namespace Storage.IntegrationEvents.Handlers
{
    public class EmailConfirmationRequestIntegrationEventHandler
        : IIntegrationEventHandler<EmailConfirmationRequestIntegrationEvent>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILogger<EmailConfirmationRequestIntegrationEventHandler> _logger;

        public EmailConfirmationRequestIntegrationEventHandler(
            UserManager<ApplicationUser> userManager,
            IRepositoryManager repositoryManager,
            ILogger<EmailConfirmationRequestIntegrationEventHandler> logger)
        {
            _userManager = userManager;
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        public async Task Handle(EmailConfirmationRequestIntegrationEvent @event)
        {
            _logger.LogInformation("Handling request to confirm user email for: {UserId}", @event.UserId);

            ApplicationUser? user = await _userManager.FindByIdAsync(@event.UserId);
            if (user is null) return;

            await _userManager.ConfirmEmailAsync(user, @event.Token);
        }
    }
}
