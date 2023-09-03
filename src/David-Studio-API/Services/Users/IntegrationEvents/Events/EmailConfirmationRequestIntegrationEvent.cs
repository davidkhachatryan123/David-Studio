using EventBus.Events;

namespace Users.IntegrationEvents.Events
{
    public record EmailConfirmationRequestIntegrationEvent(string UserId, string Token) : IntegrationEvent;
}
