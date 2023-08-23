using Pricing.Models;

namespace Pricing.Services
{
    public interface IServicesPricingRepository
    {
        Task<ServicesPricing?> GetCurrentPricing();
        Task<ServicesPricing> SetPricing(ServicesPricing pricing);
    }
}
