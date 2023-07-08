using Microsoft.AspNetCore.Identity;
using Portfolio.Database;

namespace Storage.Services
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationDbContext _context;

        private IImagesService _imagesRepository = null!;

        public RepositoryManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public IImagesService Images
        {
            get
            {
                _imagesRepository ??= new ImagesService(_context);

                return _imagesRepository;
            }
        }

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();
    }
}

