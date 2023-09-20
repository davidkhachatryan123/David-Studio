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
                tag.Join = JoinField.Link<Tag>(projectId);

                descriptor.Index<Tag>(op => op
                    .Document(tag)
                    .Routing(projectId)
                    .Id($"p-{projectId}_t-{tag.Id}")
                );
            }

            return await _client.BulkAsync(descriptor);
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
