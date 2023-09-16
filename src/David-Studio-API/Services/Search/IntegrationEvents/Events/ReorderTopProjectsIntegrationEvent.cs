using EventBus.Events;
using Search.Dtos;
using Search.Models;

namespace Search.IntegrationEvents.Events
{
    public record ReorderTopProjectsIntegrationEvent : IntegrationEvent
    {
        public IEnumerable<TopProjectDto> TopProjects { get; }

        public ReorderTopProjectsIntegrationEvent(IEnumerable<TopProjectDto> projects)
        {
            TopProjects = projects;
        }
    }
}
