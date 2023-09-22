using System.IdentityModel.Tokens.Jwt;
using EventBus;
using EventBus.Abstractions;
using EventBusRabbitMQ;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Services.Common.Configurations;

namespace Services.Common.Extensions
{
    public static class Extensions
    {
        public static void AddDefaultCors(this IServiceCollection services, IConfiguration configuration)
        {
            string[]? origins = configuration.GetValue<string>("AllowedOrigins")?.Split(';');

            services.AddCors(options =>
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowCredentials();

                    if (origins is not null)
                        policy.WithOrigins(origins);
                }));
        }

        public static void AddDefaultAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var identitySection = configuration.GetSection("Identity");

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                var identityUrl = identitySection.GetValue<string>("Url");
                var audience = identitySection.GetValue<string>("Audience");

                options.Authority = identityUrl;
                options.Audience = audience;
                options.RequireHttpsMetadata = false;
            });
        }

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

        public static void UseDefaultSwagger(this WebApplication app, IConfiguration configuration)
        {
            var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions.Reverse())
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());

                    var authSection = configuration.GetSection("OpenApi").GetSection("Auth");

                    if (authSection.Exists())
                    {
                        options.OAuthClientId(authSection.GetValue<string>("ClientId"));
                        options.OAuthAppName(authSection.GetValue<string>("AppName"));
                    }
                }
            });

            app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();
        }

        public static void AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            int retryCount = configuration.GetValue<int>("EventBus:RetryCount");

            services.AddSingleton<IRabbitMQPersistenConnection, RabbitMQPersistenConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<RabbitMQPersistenConnection>>();

                var factory = new ConnectionFactory()
                {
                    Uri = new Uri(configuration.GetConnectionString("EventBus")!),
                    DispatchConsumersAsync = true
                };

                return new RabbitMQPersistenConnection(factory, logger, retryCount);
            });

            services.AddSingleton<IEventBus, EventBusRabbitMQ.EventBusRabbitMQ>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistenConnection>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ.EventBusRabbitMQ>>();
                var eventBusSubscriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                return new EventBusRabbitMQ.EventBusRabbitMQ(rabbitMQPersistentConnection, eventBusSubscriptionsManager, sp, logger, retryCount);
            });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
        }
    }
}

