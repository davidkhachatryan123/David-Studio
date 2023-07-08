using System;
using Microsoft.EntityFrameworkCore;
using Portfolio.Database;
using Portfolio.Models;
using Services.Common;
using Services.Common.Models;

namespace Portfolio.Services
{
    public class ProjectsRepository : IBaseRepository<Project>
    {
        private readonly ApplicationDbContext _context;

        public ProjectsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PageData<Project>> GetAllAsync(PageOptions options)
            => await _context.Projects.Include(p => p.Tags).ToPagedAsync(options);

        public async Task<Project?> GetByIdAsync(int id)
            => await _context.Projects
                             .Include(p => p.Tags)
                             .FirstOrDefaultAsync(proj => proj.Id == id);

        public async Task<Project?> CreateAsync(Project project)
        {
            int[] tagIds = project.Tags.Select(p => p.Id).ToArray();

            ICollection<Tag> tags =
                await _context.Tags
                .Where(t => tagIds.Contains(t.Id))
                .ToArrayAsync();

            project.Tags = tags;
            await _context.Projects.AddAsync(project);

            return project;
        }

        public async Task<Project?> UpdateAsync(Project project)
        {
            Project? projDb = await _context.Projects
                .AsNoTracking()
                .Include(p => p.ProjectTags)
                .FirstOrDefaultAsync(p => p.Id == project.Id);
            if (projDb is null) return null;

            foreach (Tag tag in project.Tags)
                _context.Entry(tag).State = EntityState.Unchanged;

            int[] tagIds = projDb.ProjectTags.Select(p => p.TagId).ToArray();

            _context.ProjectTags.RemoveRange(
                projDb.ProjectTags.Where(pt => tagIds.Contains(pt.TagId)));
            await _context.SaveChangesAsync();

            _context.Projects.Update(project);

            return project;
        }

        public async Task<Project?> DeleteAsync(int id)
        {
            Project? project = await GetByIdAsync(id);
            if (project is null) return null;

            _context.Projects.Remove(project);

            return project;
        }
    }
}

