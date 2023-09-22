using EventBus.Events;

namespace Search.IntegrationEvents.Events
{
    public record RemoveProjectIntegrationEvent : IntegrationEvent
    {
        public int ProjectId { get; }

        public RemoveProjectIntegrationEvent(int projectId)
        {
            ProjectId = projectId;
        }
    }
}
