using EventBus.Events;
using Portfolio.Dtos;

namespace Portfolio.IntegrationEvents.Events
{
    public record UpdateProjectIntegrationEvent : IntegrationEvent
    {
        public ProjectReadDto Project { get; }

        public UpdateProjectIntegrationEvent(ProjectReadDto project)
        {
            Project = project;
        }
    }
}
