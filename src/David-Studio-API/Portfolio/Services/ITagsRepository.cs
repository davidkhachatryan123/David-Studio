using Portfolio.Models;
using Services.Common.Models;

namespace Portfolio.Services
{
    public interface ITagsRepository
    {
        Task<PageData<Tag>> GetAllAsync(PageOptions options);
        Task<Tag?> GetByIdAsync(int id);
        Task<Tag?> CreateAsync(Tag tag);
        Task<Tag?> UpdateAsync(Tag tag);
        Task<Tag?> DeleteAsync(int id);
    }
}

