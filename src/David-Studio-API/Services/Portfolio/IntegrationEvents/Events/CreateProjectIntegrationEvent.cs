using EventBus.Events;
using Portfolio.Dtos;

namespace Portfolio.IntegrationEvents.Events
{
    public record CreateProjectIntegrationEvent : IntegrationEvent
    {
        public ProjectReadDto Project { get; }

        public CreateProjectIntegrationEvent(ProjectReadDto project)
        {
            Project = project;
        }
    }
}
