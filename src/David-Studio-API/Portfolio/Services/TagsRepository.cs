using Microsoft.EntityFrameworkCore;
using Portfolio.Database;
using Portfolio.Models;
using Services.Common;
using Services.Common.Models;

namespace Portfolio.Services
{
    public class TagsRepository : IBaseRepository<Tag>
    {
        private readonly ApplicationDbContext _context;

        public TagsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PageData<Tag>> GetAllAsync(PageOptions options)
            => await _context.Tags.ToPagedAsync(options);

        public async Task<Tag?> GetByIdAsync(int id)
            => await _context.Tags.FirstOrDefaultAsync(tag => tag.Id == id);

        public async Task<Tag?> CreateAsync(Tag tag)
        {
            Tag? tagDb = await _context.Tags.FirstOrDefaultAsync(t => t.Name == tag.Name);

            if (tagDb != null) return null;

            await _context.Tags.AddAsync(tag);
            return tag;
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            Tag? tagDb = await _context.Tags
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == tag.Id);

            if (tagDb is null) return null;

            _context.Tags.Update(tag);
            return tag;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Tag? tag = await GetByIdAsync(id);
            if (tag is null) return false;

            _context.Tags.Remove(tag);

            return true;
        }
    }
}

