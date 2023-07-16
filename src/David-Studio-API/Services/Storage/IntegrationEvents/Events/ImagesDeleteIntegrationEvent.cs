using EventBus.Events;

namespace Storage.IntegrationEvents.Events
{
    public record ImagesDeleteIntegrationEvent : IntegrationEvent
    {
        public string ImageUrl { get; }

        public ImagesDeleteIntegrationEvent(string imageUrl)
        {
            ImageUrl = imageUrl;
        }
    }
}

