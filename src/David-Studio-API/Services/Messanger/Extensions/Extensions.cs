using EventBus.Abstractions;
using Messanger.IntegrationEvents.Events;
using Messanger.IntegrationEvents.Handlers;

namespace Messanger.Extensions
{
    public static class Extensions
    {
        public static void ConfigureEventBus(this WebApplication app)
        {
            var eventBus = app.Services.GetRequiredService<IEventBus>();

            eventBus.Subscribe<SendEmailIntegrationEvent, IIntegrationEventHandler<SendEmailIntegrationEvent>>();
        }
    }
}
