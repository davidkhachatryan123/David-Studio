using EventBus.Events;

namespace Portfolio.IntegrationEvents.Events
{
    public record RemoveProjectIntegrationEvent : IntegrationEvent
    {
        public int ProjectId { get; }

        public RemoveProjectIntegrationEvent(int id)
        {
            ProjectId = id;
        }
    }
}
