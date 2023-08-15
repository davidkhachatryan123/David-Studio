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
            CreateMap<User, UserReadDto>();
            CreateMap<UserCreateDto, User>();

            CreateMap<UserCreateDto, UserCreateData>();
            CreateMap<UserReadData, UserReadDto>();

            CreateMap<PageOptions, PageData>();
        }
    }
}

