using EventBus.Events;
using Search.Dtos;

namespace Search.IntegrationEvents.Events
{
    public record IndexProjectIntegrationEvent : IntegrationEvent
    {
        public ProjectDto Project { get; }

        public IndexProjectIntegrationEvent(ProjectDto project)
        {
            Project = project;
        }
    }
}
