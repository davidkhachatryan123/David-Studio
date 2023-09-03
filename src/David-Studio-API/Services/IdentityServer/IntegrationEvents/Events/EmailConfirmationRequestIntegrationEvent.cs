using EventBus.Events;

namespace IdentityServer.IntegrationEvents.Events
{
    public record EmailConfirmationRequestIntegrationEvent(string UserId, string Token) : IntegrationEvent;
}
