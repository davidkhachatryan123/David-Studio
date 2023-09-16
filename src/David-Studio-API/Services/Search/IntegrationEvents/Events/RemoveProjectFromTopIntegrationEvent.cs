using EventBus.Events;

namespace Search.IntegrationEvents.Events
{
    public record RemoveProjectFromTopIntegrationEvent : IntegrationEvent
    {
        public int ProjectId { get; }

        public RemoveProjectFromTopIntegrationEvent(int id)
        {
            ProjectId = id;
        }
    }
}
