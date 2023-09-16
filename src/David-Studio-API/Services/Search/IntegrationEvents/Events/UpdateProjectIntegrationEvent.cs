using EventBus.Events;
using Search.Models;

namespace Search.IntegrationEvents.Events
{
    public record UpdateProjectIntegrationEvent : IntegrationEvent
    {
        public Project Project { get; }

        public UpdateProjectIntegrationEvent(Project project)
        {
            Project = project;
        }
    }
}
