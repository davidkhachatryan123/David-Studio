namespace Pricing.Services.RepositoryManager
{
    public interface IRepositoryManager
    {
        IServicesPricingRepository ServicesPricing { get; }

        Task SaveAsync();
    }
}

