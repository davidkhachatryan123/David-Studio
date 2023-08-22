using System.Reflection;
using AutoMapper;
using EventBus.Abstractions;
using Messanger.Database;
using Messanger.IntegrationEvents.Events;
using Messanger.IntegrationEvents.Handlers;
using Messanger.Mappings;
using Messanger.Options;
using Microsoft.EntityFrameworkCore;

namespace Messanger.Extensions
{
    public static class Extensions
    {
        public static void ConfigureDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MessagesDb"),
                x => x.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name)));
        }

        public static void MigrateDatabase(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.Migrate();
            }
        }

        public static void ConfigureMapping(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(map =>
            {
                map.AddProfile<ContactMappingProfile>();
            });

            services.AddSingleton(mapperConfig.CreateMapper());
        }

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
