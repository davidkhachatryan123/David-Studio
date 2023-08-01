namespace Messanger.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string to, string subject, string body, bool withPrefix = true);
    }
}
