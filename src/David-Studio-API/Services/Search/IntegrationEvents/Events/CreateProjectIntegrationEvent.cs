using EventBus.Events;
using Search.Models;

namespace Search.IntegrationEvents.Events
{
    public record CreateProjectIntegrationEvent : IntegrationEvent
    {
        public Project Project { get; }

        public CreateProjectIntegrationEvent(Project project)
        {
            Project = project;
        }
    }
}
