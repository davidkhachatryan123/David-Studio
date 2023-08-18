using AutoMapper;
using IdentityServer.Models;
using Services.Common.Models;

namespace IdentityServer.Mappings
{
    public class UsersMappingProfile : Profile
    {
        public UsersMappingProfile()
        {
            CreateMap<ApplicationUser, AdminReadData>()
                .AddTransform<string?>(value => value ?? string.Empty);
            CreateMap<AdminCreateData, ApplicationUser>();

            CreateMap<PageData, PageOptions>();
        }
    }
}

