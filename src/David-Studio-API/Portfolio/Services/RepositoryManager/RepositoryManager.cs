using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Portfolio.Database;
using Portfolio.Models;

namespace Portfolio.Services
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationDbContext _context;

        private IBaseRepository<Tag> _tagsRepository = null!;
        private IBaseRepository<Project> _projectsRespository = null!;

        public RepositoryManager(ApplicationDbContext context)
        {
            _context = context;
        }


        public IBaseRepository<Tag> Tags
        {
            get
            {
                _tagsRepository ??= new TagsRepository(_context);

                return _tagsRepository;
            }
        }

        public IBaseRepository<Project> Projects
        {
            get
            {
                _projectsRespository ??= new ProjectsRepository(_context);

                return _projectsRespository;
            }
        }

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();
    }
}

