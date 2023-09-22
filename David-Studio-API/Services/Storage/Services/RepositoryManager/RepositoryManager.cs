using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Portfolio.Database;
using Storage.Options;

namespace Storage.Services
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationDbContext _context;
        private readonly IOptions<StorageOptions> _storageOptions;
        private readonly ILogger<RepositoryManager> _logger;

        private IImagesService _imagesRepository = null!;

        public RepositoryManager(
            ApplicationDbContext context,
            IOptions<StorageOptions> storageOptions,
            ILogger<RepositoryManager> logger)
        {
            _context = context;
            _storageOptions = storageOptions;
            _logger = logger;
        }

        public IImagesService Images
        {
            get
            {
                _imagesRepository ??= new ImagesService(_context, _storageOptions, _logger);

                return _imagesRepository;
            }
        }

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();
    }
}

