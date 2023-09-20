using AutoMapper;
using Elasticsearch.Net;
using EventBus.Abstractions;
using Nest;
using Portfolio.Mappings;
using Search.IntegrationEvents.Events;
using Search.IntegrationEvents.Handlers;
using Search.Models;

namespace Search.Extensions
{
    public static class Extensions
    {
        public static void AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionPool = new SingleNodeConnectionPool(new Uri(configuration["ElasticSearchUrl"]!));

            var connectionSettings = new ConnectionSettings(connectionPool)
                .DefaultMappingFor<ProjectBase>(m => m.IndexName("projects"))
                .DefaultMappingFor<Tag>(m => m.IndexName("projects").RelationName("tag"))
                .DefaultMappingFor<Project>(m => m.IndexName("projects").RelationName("project"))
                .DefaultIndex("projects")
                .EnableApiVersioningHeader();

            var client = new ElasticClient(connectionSettings);

            services.AddSingleton(client);
        }

        public static void AddEventBusHandlers(this IServiceCollection services)
        {
            services.AddTransient<IIntegrationEventHandler<IndexProjectIntegrationEvent>,
                IndexProjectIntegrationEventHandler>();
            services.AddTransient<IIntegrationEventHandler<RemoveProjectIntegrationEvent>,
               RemoveProjectIntegrationEventHandler>();

            services.AddTransient<IIntegrationEventHandler<UpdateTagIntegrationEvent>,
                UpdateTagIntegrationEventHandler>();
            services.AddTransient<IIntegrationEventHandler<RemoveTagIntegrationEvent>,
               RemoveTagIntegrationEventHandler>();

            services.AddTransient<IIntegrationEventHandler<IndexTopProjectsIntegrationEvent>,
                IndexTopProjectsIntegrationEventHandler>();
        }

        public static void AddEventBusSubscriptions(this WebApplication app)
        {
            var eventBus = app.Services.GetRequiredService<IEventBus>();

            eventBus.Subscribe<IndexProjectIntegrationEvent,
                IIntegrationEventHandler<IndexProjectIntegrationEvent>>();
            eventBus.Subscribe<RemoveProjectIntegrationEvent,
                IIntegrationEventHandler<RemoveProjectIntegrationEvent>>();

            eventBus.Subscribe<UpdateTagIntegrationEvent,
                IIntegrationEventHandler<UpdateTagIntegrationEvent>>();
            eventBus.Subscribe<RemoveTagIntegrationEvent,
                IIntegrationEventHandler<RemoveTagIntegrationEvent>>();

            eventBus.Subscribe<IndexTopProjectsIntegrationEvent,
                IIntegrationEventHandler<IndexTopProjectsIntegrationEvent>>();
        }

        public static void AddMappings(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(map =>
            {
                map.AddProfile<ProjectMappingProfile>();
            });

            services.AddSingleton(mapperConfig.CreateMapper());
        }
    }
}
