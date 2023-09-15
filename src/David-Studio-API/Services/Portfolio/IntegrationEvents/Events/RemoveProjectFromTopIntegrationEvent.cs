using EventBus.Events;

namespace Portfolio.IntegrationEvents.Events
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
