using System.Net.Mail;
using EventBus.Abstractions;
using Messanger.IntegrationEvents.Events;
using Messanger.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Messanger.IntegrationEvents.Handlers
{
    public class SendEmailIntegrationEventHandler
        : IIntegrationEventHandler<SendEmailIntegrationEvent>
    {
        private readonly SmtpOptions _smtpOptions;
        private readonly IConfiguration _configuration;
        private readonly ILogger<SendEmailIntegrationEventHandler> _logger;

        public SendEmailIntegrationEventHandler(
            IOptions<SmtpOptions> smtpOptions,
            IConfiguration configuration,
            ILogger<SendEmailIntegrationEventHandler> logger)
        {
            _smtpOptions = smtpOptions.Value;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task Handle(SendEmailIntegrationEvent @event)
        {
            _logger.LogInformation("Send email to: {EmailAddress}", @event.To);

            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(_smtpOptions.From);
                mailMessage.To.Add(new MailAddress(@event.To));

                mailMessage.Subject = @event.Subject;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = @event.Body;

                using (SmtpClient client = new SmtpClient(_smtpOptions.Host, int.Parse(_smtpOptions.Port)))
                {
                    client.EnableSsl = true;
                    client.Credentials = new System.Net.NetworkCredential(_smtpOptions.Username, _smtpOptions.Password);

                    try
                    {
                        await client.SendMailAsync(mailMessage);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("Error occurred when sending email to: {EmailAddress}; Exception message: {ExceptionMessage}", @event.To, ex.Message);
                    }
                }
            }
        }
    }
}
