using Nest;
using Search.Models;

namespace Search.Services
{
    public class TagsRepository : ITagsRepository
    {
        private readonly ElasticClient _client;

        public TagsRepository(ElasticClient client)
        {
            _client = client;
        }

        public async Task<BulkResponse> IndexRangeAsync(IEnumerable<Tag> tags, int projectId)
        {
            var descriptor = new BulkDescriptor();

            foreach (var tag in tags)
            {
                tag.Join = JoinField.Link<Tag>($"p-{projectId}");

                descriptor.Index<Tag>(op => op
                    .Document(tag)
                    .Routing(projectId)
                    .Id($"p-{projectId}_t-{tag.Id}")
                );
            }

            return await _client.BulkAsync(descriptor);
        }

        public async Task<BulkResponse> UpdateTagsAsync(Tag tag)
        {
            var response = await _client.SearchAsync<Tag>(uq => uq
                .Query(q => q
                    .Bool(b => b
                        .Must(m => m
                            .Match(m => m
                                .Field(p => p.Id)
                                .Query(tag.Id.ToString())
                            )
                        )
                        .MustNot(m => m
                            .Match(m => m
                                .Field(p => p.Join)
                                .Query(nameof(Project))
                            )
                        )
                    )
                )
            );


            var descriptor = new BulkDescriptor();

            foreach (var tagHit in response.Hits)
            {
                tagHit.Source.Name = tag.Name;
                tagHit.Source.Color = tag.Color;

                descriptor.Index<Tag>(op => op
                    .Document(tagHit.Source)
                    .Routing(tagHit.Routing)
                    .Id($"p-{tagHit.Routing}_t-{tag.Id}")
                );
            }

            return await _client.BulkAsync(descriptor);
        }

        public async Task<long> DeleteTagsAsync(int tagId)
        {
            var response = await _client.DeleteByQueryAsync<Tag>(q => q
                .Query(q => q
                    .Bool(b => b
                        .Must(m => m
                            .Match(m => m
                                .Field(p => p.Id)
                                .Query(tagId.ToString())
                            )
                        )
                        .MustNot(m => m
                            .Match(m => m
                                .Field(p => p.Join)
                                .Query(nameof(Project))
                            )
                        )
                    )
                )
            );

            return response.Deleted;
        }

        public async Task<long> ClearTagsAsync(int projectId)
        {
            var response = await _client.DeleteByQueryAsync<Tag>(q => q
                .Query(rq => rq
                    .ParentId(pq => pq
                        .Type<Tag>()
                        .Id(projectId.ToString())
                    )
                )
            );

            return response.Deleted;
        }
    }
}
