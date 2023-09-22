using EventBus.Events;

namespace Portfolio.IntegrationEvents.Events
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

