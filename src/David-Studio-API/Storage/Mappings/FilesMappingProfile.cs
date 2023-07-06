using AutoMapper;
using Storage;
using Storage.Models;

namespace Portfolio.Mappings
{
    public class FilesMappingProfile : Profile
    {
        public FilesMappingProfile()
        {
            CreateMap<Image, GrpcImageModel>();
        }
    }
}

