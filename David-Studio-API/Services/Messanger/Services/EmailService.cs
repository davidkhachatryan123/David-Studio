using Messanger.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace Messanger.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpOptions _smtpOptions;
        private readonly EmailOptions _emailOptions;
        private readonly ILogger<EmailService> _logger;

        public EmailService(
            IOptions<SmtpOptions> smtpOptions,
            IOptions<EmailOptions> emailOptions,
            ILogger<EmailService> logger)
        {
            _smtpOptions = smtpOptions.Value;
            _emailOptions = emailOptions.Value;
            _logger = logger;
        }

        public async Task<bool> SendEmailAsync(string from, string to, string subject, string body, bool withPrefix = true)
        {
            _logger.LogInformation("Send email to: {EmailAddress}", to);

            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(from);
                mailMessage.To.Add(new MailAddress(to));

                mailMessage.Subject = withPrefix ? $"{_emailOptions.Prefix} - {subject}" : subject;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = body;

                using (SmtpClient client = new SmtpClient(_smtpOptions.Host, int.Parse(_smtpOptions.Port)))
                {
                    client.EnableSsl = true;
                    client.Credentials = new System.Net.NetworkCredential(_smtpOptions.Username, _smtpOptions.Password);

                    try
                    {
                        await client.SendMailAsync(mailMessage);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("Error occurred when sending email to: {EmailAddress}; Exception message: {ExceptionMessage}", to, ex.Message);
                        return false;
                    }
                }
            }
        }
    }
}
