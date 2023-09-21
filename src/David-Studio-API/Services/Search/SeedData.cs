using Nest;
using Search.Models;

namespace Search
{
    public static class SeedData
    {
        public static async Task SeedIndices(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                ElasticClient client =
                    scope.ServiceProvider.GetRequiredService<ElasticClient>();

                if (!client.Indices.ExistsAsync("projects").Result.Exists)
                {
                    var createIndexResponse = await client.Indices.CreateAsync("projects", c => c
                        .Index<ProjectBase>()
                        .Map<ProjectBase>(m => m
                            .RoutingField(r => r.Required())
                            .AutoMap<Project>()
                            .AutoMap<Tag>()
                            .Properties(props => props
                                .Join(j => j
                                    .Name(p => p.Join)
                                    .Relations(r => r
                                        .Join<Project, Tag>()
                                        )
                                    )
                                )
                            )
                        );

                    if (!createIndexResponse.Acknowledged)
                        throw new Exception("Error occurred in ElasticSearch when trying to create index");
                }
            }
        }
    }
}
