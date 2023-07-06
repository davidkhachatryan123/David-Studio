using AutoMapper;
using Portfolio.Dtos;
using Portfolio.Models;

namespace Portfolio.Mappings
{
    public class FilesMappingProfile : Profile
    {
        public FilesMappingProfile()
        {
            CreateMap<GrpcImageModel, ImageReadDto>();
        }
    }
}

