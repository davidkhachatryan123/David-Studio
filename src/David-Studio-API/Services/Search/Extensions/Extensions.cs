using Elasticsearch.Net;
using Nest;
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
                .DefaultMappingFor<Project>(m => m.IndexName("projects").RelationName("project"));

            var client = new ElasticClient(connectionSettings);

            services.AddSingleton(client);
        }
    }
}
