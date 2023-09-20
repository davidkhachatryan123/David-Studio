using AutoMapper;
using Search.Dtos;
using Search.Models;

namespace Portfolio.Mappings
{
    public class ProjectMappingProfile : Profile
    {
        public ProjectMappingProfile()
        {
            CreateMap<ProjectDto, Project>();
        }
    }
}
