using EventBus.Events;

namespace Search.IntegrationEvents.Events
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
