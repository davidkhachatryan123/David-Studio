using EventBus.Events;
using Portfolio.Models;

namespace Portfolio.IntegrationEvents.Events
{
    public record ReorderTopProjectsIntegrationEvent : IntegrationEvent
    {
        public IEnumerable<TopProject> TopProjects { get; }

        public ReorderTopProjectsIntegrationEvent(IEnumerable<TopProject> projects)
        {
            TopProjects = projects;
        }
    }
}
