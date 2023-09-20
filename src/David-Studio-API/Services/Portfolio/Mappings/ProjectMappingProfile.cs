using AutoMapper;
using Portfolio.Dtos;
using Portfolio.Models;

namespace Portfolio.Mappings
{
    public class ProjectMappingProfile : Profile
    {
        public ProjectMappingProfile()
        {
            CreateMap<Project, ProjectReadDto>();
            CreateMap<ProjectCreateDto, Project>();

            CreateMap<TopProject, TopProjectDto>();
        }
    }
}
