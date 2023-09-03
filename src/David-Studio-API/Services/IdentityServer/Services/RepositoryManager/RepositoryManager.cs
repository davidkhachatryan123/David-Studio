using EventBus.Abstractions;
using IdentityServer.Database;
using IdentityServer.Models;
using IdentityServer.Services;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.RepositoryManager.Services
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<RepositoryManager> _logger;

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEventBus _eventBus;

        private IManageUsersRepository _manageUsersRespository = null!;

        public RepositoryManager(
            LinkGenerator linkGenerator,
            IHttpContextAccessor httpContextAccessor,
            ILogger<RepositoryManager> logger,
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IEventBus eventBus
           )
        {
            _linkGenerator = linkGenerator;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;

            _context = context;
            _userManager = userManager;
            _eventBus = eventBus;
        }

        public IManageUsersRepository ManageUsers
        {
            get
            {
                _manageUsersRespository ??= new ManageUsersRepository(_linkGenerator, _httpContextAccessor, _logger, _userManager, _eventBus);

                return _manageUsersRespository;
            }
        }

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();
    }
}
