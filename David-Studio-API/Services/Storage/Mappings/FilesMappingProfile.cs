using AutoMapper;
using Storage;
using Storage.Models;

namespace Portfolio.Mappings
{
    public class FilesMappingProfile : Profile
    {
        public FilesMappingProfile(IConfiguration configuration)
        {
            CreateMap<Image, GrpcImageModel>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(
                    src => $"{configuration.GetValue<string>("Resources_Url")}/Images/{src.UniqueName}"));
        }
    }
}

