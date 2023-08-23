using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pricing.Dtos;
using Pricing.Models;
using Pricing.Services.RepositoryManager;
using Services.Common.Models;

namespace Messanger.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ServicesPricingController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly ILogger<ServicesPricingController> _logger;

        public ServicesPricingController(
            IRepositoryManager repositoryManager,
            IMapper mapper,
            ILogger<ServicesPricingController> logger)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _logger = logger;
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        public async Task<IActionResult> GetPrices()
        {
            ServicesPricing? pricing =
                await _repositoryManager.ServicesPricing.GetCurrentPricing();

            if (pricing is null)
                return NotFound("Not found any pricing plans");

            _logger.LogInformation("Get current pricing changed on: {ChangeDate}", pricing.ChangeDate);

            return Ok(_mapper.Map<ServicesPricingReadDto>(pricing));
        }

        [MapToApiVersion("1.0")]
        [HttpPost]
        public async Task<IActionResult> SetPrices(ServicesPricingCreateDto newPricing)
        {
            ServicesPricing pricing =
                await _repositoryManager.ServicesPricing.SetPricing(_mapper.Map<ServicesPricing>(newPricing));

            await _repositoryManager.SaveAsync();

            _logger.LogInformation("Changed pricing on: {ChangeDate}", pricing.ChangeDate);

            return Ok(_mapper.Map<ServicesPricingReadDto>(pricing));
        }
    }
}
