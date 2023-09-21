using System.Text.Json.Serialization;
using EventBus.Events;
using Search.Dtos;

namespace Search.IntegrationEvents.Events
{
    public record IndexTopProjectsIntegrationEvent : IntegrationEvent
    {
        public IEnumerable<TopProjectDto> TopProjects { get; }
        public int? RemovedTopProjectId { get; }

        public IndexTopProjectsIntegrationEvent(IEnumerable<TopProjectDto> topProjects)
        {
            TopProjects = topProjects;
        }

        [JsonConstructor]
        public IndexTopProjectsIntegrationEvent(
            int? removedTopProjectId,
            IEnumerable<TopProjectDto> topProjects)
        {
            RemovedTopProjectId = removedTopProjectId;
            TopProjects = topProjects;
        }
    }
}
