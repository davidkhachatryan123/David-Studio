using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Services.Common.Configurations;
using Services.Common.EventBus;

namespace Services.Common
{
    public static class Extensions
    {
        public static void AddDefaultApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
                opt.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("x-api-version"),
                    new MediaTypeApiVersionReader("x-api-version"));
            });

            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });
        }

        public static void AddDefaultSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.ConfigureOptions<ConfigureSwaggerOptions>();
        }

        public static void UseDefaultSwagger(this WebApplication app)
        {
            var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions.Reverse())
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
            });
        }

        public static void AddEventBus(this IServiceCollection services, IConfiguration configuration, string exchangeName)
        {
            services.AddSingleton<IMessageBusClient, MessageBusClient>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<IMessageBusClient>>();

                var factory = new ConnectionFactory()
                {
                    Uri = new Uri(configuration.GetConnectionString("EventBus")!),
                    DispatchConsumersAsync = true
                };

                return new MessageBusClient(factory, logger, configuration.GetValue<int>("EventBus:RetryCount"));
            });

            services.AddSingleton<IMessageBus, MessageBus>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IMessageBusClient>();

                return new MessageBus(rabbitMQPersistentConnection, exchangeName);
            });
        }
    }
}

