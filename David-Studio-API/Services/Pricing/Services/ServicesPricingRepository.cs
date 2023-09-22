using Messanger.Database;
using Microsoft.EntityFrameworkCore;
using Pricing.Models;

namespace Pricing.Services
{
    public class ServicesPricingRepository : IServicesPricingRepository
    {
        private readonly ApplicationDbContext _context;

        public ServicesPricingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServicesPricing?> GetCurrentPricing()
            => await _context.ServicesPricings
                             .OrderBy(p => p.ChangeDate)
                             .LastOrDefaultAsync();

        public async Task<ServicesPricing> SetPricing(ServicesPricing pricing)
        {
            await _context.ServicesPricings.AddAsync(pricing);

            return pricing;
        }
    }
}
