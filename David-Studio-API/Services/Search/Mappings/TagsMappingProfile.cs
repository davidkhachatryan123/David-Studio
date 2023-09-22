using AutoMapper;
using Search.Dtos;
using Search.Models;

namespace Search.Mappings
{
    public class TagsMappingProfile : Profile
    {
        public TagsMappingProfile()
        {
            CreateMap<TagDto, Tag>();
            CreateMap<Tag, TagDto>();
        }
    }
}
