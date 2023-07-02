using Microsoft.AspNetCore.Identity;
using Portfolio.Database;

namespace Storage.Services
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationDbContext _context;

        private IFileManagement _filesRepository = null!;

        public RepositoryManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public IFileManagement Files
        {
            get
            {
                _filesRepository ??= new FileManagement(_context);

                return _filesRepository;
            }
        }

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();
    }
}

