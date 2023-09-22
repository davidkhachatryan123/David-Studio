using EventBus.Abstractions;
using Messanger.IntegrationEvents.Events;
using Messanger.Options;
using Messanger.Services;
using Microsoft.Extensions.Options;

namespace Messanger.IntegrationEvents.Handlers
{
    public class SendTwoFactorCodeEmailIntegrationEventHandler
        : IIntegrationEventHandler<SendTwoFactorCodeEmailIntegrationEvent>
    {
        private readonly IEmailService _emailService;
        private readonly EmailOptions _emailOptions;
        private readonly ILogger<SendTwoFactorCodeEmailIntegrationEventHandler> _logger;

        public SendTwoFactorCodeEmailIntegrationEventHandler(
            IEmailService emailService,
            IOptions<EmailOptions> emailOptions,
            ILogger<SendTwoFactorCodeEmailIntegrationEventHandler> logger)
        {
            _emailService = emailService;
            _emailOptions = emailOptions.Value;
            _logger = logger;
        }

        public async Task Handle(SendTwoFactorCodeEmailIntegrationEvent @event)
        {
            string body = File.ReadAllText(@"EmailTemplates/2FACode.html")
                              .Replace("{{code}}", @event.Code.Insert(3, " - "));

            bool result = await _emailService.SendEmailAsync(_emailOptions.FromForIdentity, @event.To, "2FA Code", body);

            if (result)
                _logger.LogInformation("Email was sent successfully to: {EmailAddress}", @event.To);
            else
                _logger.LogError("Email doesn't send to: {EmailAddress}", @event.To);
        }
    }
}
