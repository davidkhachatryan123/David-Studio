using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Portfolio.Database;
using Portfolio.Models;

namespace Portfolio.Services
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        private IProjectsRepository _projectsRespository = null!;
        private ITagsRepository _tagsRepository = null!;
        private ITopProjectsRepository _topProjectsRepository = null!;

        public RepositoryManager(
            ApplicationDbContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public IProjectsRepository Projects
        {
            get
            {
                _projectsRespository ??= new ProjectsRepository(_context);

                return _projectsRespository;
            }
        }

        public ITagsRepository Tags
        {
            get
            {
                _tagsRepository ??= new TagsRepository(_context);

                return _tagsRepository;
            }
        }

        public ITopProjectsRepository TopProjects
        {
            get
            {
                _topProjectsRepository ??= new TopProjectsRepository(_context, _configuration);

                return _topProjectsRepository;
            }
        }

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();
    }
}
