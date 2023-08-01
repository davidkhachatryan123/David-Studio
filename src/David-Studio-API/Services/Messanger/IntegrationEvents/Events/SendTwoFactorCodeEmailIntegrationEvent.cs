using EventBus.Events;

namespace Messanger.IntegrationEvents.Events
{
    public record SendTwoFactorCodeEmailIntegrationEvent : IntegrationEvent
    {
        public string To { get; }
        public string Code { get; }

        public SendTwoFactorCodeEmailIntegrationEvent(string to, string code)
        {
            To = to;
            Code = code;
        }
    }
}

