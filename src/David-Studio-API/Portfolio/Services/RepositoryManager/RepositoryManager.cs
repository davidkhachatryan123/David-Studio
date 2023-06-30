using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Portfolio.Database;

namespace Portfolio.Services
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationDbContext _context;

        private ITagsRepository _tagsRepository = null!;

        public RepositoryManager(ApplicationDbContext context)
        {
            _context = context;
        }


        public ITagsRepository Tags
        {
            get
            {
                _tagsRepository ??= new TagsRepository(_context);

                return _tagsRepository;
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

