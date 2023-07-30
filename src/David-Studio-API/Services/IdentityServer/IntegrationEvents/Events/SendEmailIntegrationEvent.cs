using EventBus.Events;

namespace IdentityServer.IntegrationEvents.Events
{
    public record SendEmailIntegrationEvent : IntegrationEvent
    {
        public string To { get; }
        public string Subject { get; }
        public string Body { get; }

        public SendEmailIntegrationEvent(string to, string subject, string body)
        {
            To = to;
            Subject = subject;
            Body = body;
        }
    }
}

