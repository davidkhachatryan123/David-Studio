namespace Messanger.Options
{
    public class SmtpOptions
    {
        public string From { get; set; } = null!;
        public string Host { get; set; } = null!;
        public string Port { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}

