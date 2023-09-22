using EventBus.Events;
using Portfolio.Dtos;

namespace Portfolio.IntegrationEvents.Events
{
    public record IndexProjectIntegrationEvent : IntegrationEvent
    {
        public ProjectReadDto Project { get; }

        public IndexProjectIntegrationEvent(ProjectReadDto project)
        {
            Project = project;
        }
    }
}
