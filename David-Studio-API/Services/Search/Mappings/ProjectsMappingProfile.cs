using AutoMapper;
using Search.Dtos;
using Search.Models;

namespace Search.Mappings
{
    public class ProjectsMappingProfile : Profile
    {
        public ProjectsMappingProfile()
        {
            CreateMap<ProjectDto, Project>();
            CreateMap<Project, ProjectDto>();
        }
    }
}
