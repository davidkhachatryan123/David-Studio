using EventBus.Events;

namespace Portfolio.IntegrationEvents.Events
{
    public record RemoveTagIntegrationEvent : IntegrationEvent
    {
        public int TagId { get; }

        public RemoveTagIntegrationEvent(int tagId)
        {
            TagId = tagId;
        }
    }
}
