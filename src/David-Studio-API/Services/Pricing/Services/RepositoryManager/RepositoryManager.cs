using Messanger.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Pricing.Services.RepositoryManager
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationDbContext _context;

        private IServicesPricingRepository _servicesPricingRespository = null!;

        public RepositoryManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public IServicesPricingRepository ServicesPricing
        {
            get
            {
                _servicesPricingRespository ??= new ServicesPricingRepository(_context);

                return _servicesPricingRespository;
            }
        }

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();
    }
}
