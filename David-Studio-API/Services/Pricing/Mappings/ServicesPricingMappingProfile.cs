using AutoMapper;
using Pricing.Dtos;
using Pricing.Models;

namespace Messanger.Mappings
{
    public class ServicesPricingMappingProfile : Profile
    {
        public ServicesPricingMappingProfile()
        {
            CreateMap<ServicesPricingCreateDto, ServicesPricing>();
            CreateMap<ServicesPricing, ServicesPricingReadDto>();
        }
    }
}
