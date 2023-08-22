using EventBus.Abstractions;
using Messanger.IntegrationEvents.Events;
using Messanger.Options;
using Messanger.Services;
using Microsoft.Extensions.Options;

namespace Messanger.IntegrationEvents.Handlers
{
    public class SendConfirmationEmailIntegrationEventHandler
        : IIntegrationEventHandler<SendConfirmationEmailIntegrationEvent>
    {
        private readonly IEmailService _emailService;
        private readonly EmailOptions _emailOptions;
        private readonly ILogger<SendConfirmationEmailIntegrationEventHandler> _logger;

        public SendConfirmationEmailIntegrationEventHandler(
            IEmailService emailService,
            IOptions<EmailOptions> emailOptions,
            ILogger<SendConfirmationEmailIntegrationEventHandler> logger)
        {
            _emailService = emailService;
            _emailOptions = emailOptions.Value;
            _logger = logger;
        }

        public async Task Handle(SendConfirmationEmailIntegrationEvent @event)
        {
            string body = File.ReadAllText(@"EmailTemplates/EmailConfirmation.html")
                              .Replace("{{link}}", @event.Url);

            bool result = await _emailService.SendEmailAsync(_emailOptions.FromForIdentity, @event.To, "Please confirm your email", body);

            if (result)
                _logger.LogInformation("Email was sent successfully to: {EmailAddress}", @event.To);
            else
                _logger.LogError("Email doesn't send to: {EmailAddress}", @event.To);
        }
    }
}
