using EventBus.Events;
using Portfolio.Dtos;

namespace Portfolio.IntegrationEvents.Events
{
    public record UpdateTagIntegrationEvent : IntegrationEvent
    {
        public TagReadDto Tag;

        public UpdateTagIntegrationEvent(TagReadDto tag)
        {
            Tag = tag;
        }
    }
}
