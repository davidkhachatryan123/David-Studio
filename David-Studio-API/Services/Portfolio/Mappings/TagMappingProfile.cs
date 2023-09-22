using AutoMapper;
using Portfolio.Dtos;
using Portfolio.Models;

namespace Portfolio.Mappings
{
    public class TagMappingProfile : Profile
    {
        public TagMappingProfile()
        {
            CreateMap<Tag, TagReadDto>();
            CreateMap<TagCreateDto, Tag>();

            CreateMap<TagReadDto, Tag>();
        }
    }
}

