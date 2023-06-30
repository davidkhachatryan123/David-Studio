using Portfolio.Dtos;
using Portfolio.Models;

namespace Portfolio.Services
{
    public interface ITagsRepository
    {
        Task<TablesDataDto<Tag>> GetAllAsync(TableOptionsDto options);
        Task<Tag?> GetByIdAsync(int id);
        Task<Tag?> CreateAsync(Tag tag);
        Task<Tag?> UpdateAsync(Tag tag);
        Task<bool> DeleteAsync(int id);
    }
}

