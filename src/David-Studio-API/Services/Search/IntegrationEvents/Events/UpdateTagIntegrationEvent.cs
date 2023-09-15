using EventBus.Events;
using Search.Models;

namespace Portfolio.IntegrationEvents.Events
{
    public record UpdateTagIntegrationEvent : IntegrationEvent
    {
        public Tag Tag;

        public UpdateTagIntegrationEvent(Tag tag)
        {
            Tag = tag;
        }
    }
}
