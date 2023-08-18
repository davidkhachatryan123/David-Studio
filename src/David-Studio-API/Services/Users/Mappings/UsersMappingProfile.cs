using AutoMapper;
using Services.Common.Models;
using Users.Dtos;
using Users.Models;

namespace Users.Mappings
{
    public class UsersMappingProfile : Profile
    {
        public UsersMappingProfile()
        {
            CreateMap<Admin, AdminReadDto>();
            CreateMap<AdminCreateDto, Admin>();

            CreateMap<AdminReadData, AdminReadDto>();
            CreateMap<AdminCreateDto, AdminCreateData>()
                .AddTransform<string?>(value => value ?? string.Empty);

            CreateMap<PageOptions, PageData>();
        }
    }
}

