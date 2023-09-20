using EventBus.Events;
using Portfolio.Dtos;

namespace Portfolio.IntegrationEvents.Events
{
    public record IndexTopProjectsIntegrationEvent : IntegrationEvent
    {
        public IEnumerable<TopProjectDto> TopProjects { get; }
        public int? RemovedTopProjectId { get; }

        public IndexTopProjectsIntegrationEvent(IEnumerable<TopProjectDto> topProjects)
        {
            TopProjects = topProjects;
        }

        public IndexTopProjectsIntegrationEvent(
            int removedTopProjectId,
            IEnumerable<TopProjectDto> topProjects)
        {
            RemovedTopProjectId = removedTopProjectId;
            TopProjects = topProjects;
        }
    }
}
