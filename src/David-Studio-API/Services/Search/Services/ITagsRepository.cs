using Nest;
using Search.Models;

namespace Search.Services
{
    public interface ITagsRepository
    {
        Task<BulkResponse> IndexRangeAsync(IEnumerable<Tag> tags, int projectId);
        Task<BulkResponse> UpdateTagsAsync(Tag tag);

        Task<long> DeleteTagsAsync(int tagId);
        Task<long> ClearTagsAsync(int projectId);
    }
}
