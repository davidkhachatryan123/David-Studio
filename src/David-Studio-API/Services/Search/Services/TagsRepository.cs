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

        public Task<Tag?> GetIndexByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Tag> CreateIndexAsync(Tag Tag)
        {
            throw new NotImplementedException();
        }

        public Task<Tag> UpdateIndexAsync(Tag Tag)
        {
            throw new NotImplementedException();
        }

        public Task<Tag> DeleteIndexAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
