using EventBus.Events;

namespace IdentityServer.IntegrationEvents.Events
{
    public record SendConfirmationEmailIntegrationEvent : IntegrationEvent
    {
        public string To { get; }
        public string Url { get; }

        public SendConfirmationEmailIntegrationEvent(string to, string url)
        {
            To = to;
            Url = url;
        }
    }
}
