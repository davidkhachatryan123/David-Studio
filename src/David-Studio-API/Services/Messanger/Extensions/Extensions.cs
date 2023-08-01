using EventBus.Abstractions;
using Messanger.IntegrationEvents.Events;
using Messanger.IntegrationEvents.Handlers;
using Messanger.Options;

namespace Messanger.Extensions
{
    public static class Extensions
    {
        public static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SmtpOptions>(configuration.GetSection(nameof(SmtpOptions)));
            services.Configure<EmailOptions>(configuration.GetSection(nameof(EmailOptions)));
        }

        public static void AddEventBusHandlers(this IServiceCollection services)
        {
            services.AddTransient
                <IIntegrationEventHandler<SendConfirmationEmailIntegrationEvent>,
                SendConfirmationEmailIntegrationEventHandler>();

            services.AddTransient
                <IIntegrationEventHandler<SendTwoFactorCodeEmailIntegrationEvent>,
                SendTwoFactorCodeEmailIntegrationEventHandler>();
        }

        public static void ConfigureEventBus(this WebApplication app)
        {
            var eventBus = app.Services.GetRequiredService<IEventBus>();

            eventBus.Subscribe<SendConfirmationEmailIntegrationEvent, IIntegrationEventHandler<SendConfirmationEmailIntegrationEvent>>();
            eventBus.Subscribe<SendTwoFactorCodeEmailIntegrationEvent, IIntegrationEventHandler<SendTwoFactorCodeEmailIntegrationEvent>>();
        }
    }
}
