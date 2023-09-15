using Search.Models;
using Services.Common.Models;

namespace Search.Services
{
    public interface ITagsRepository
    {
        Task<Tag?> GetIndexByIdAsync(int id);
        Task<Tag> CreateIndexAsync(Tag Tag);
        Task<Tag> UpdateIndexAsync(Tag Tag);
        Task<Tag> DeleteIndexAsync(int id);
    }
}
