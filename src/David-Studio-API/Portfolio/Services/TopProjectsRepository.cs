using Microsoft.EntityFrameworkCore;
using Portfolio.Database;
using Portfolio.Models;

namespace Portfolio.Services
{
    public class TopProjectsRepository : ITopProjectsRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly int MaxTopProjectsCount;

        public TopProjectsRepository(
            ApplicationDbContext context,
            IConfiguration configuration)
        {
            _context = context;

            MaxTopProjectsCount = configuration.GetValue<int>("MaxTopProjectsCount");
        }

        public async Task<IEnumerable<Project>> GetAllAsync(int? limit = null)
        {
            IQueryable<Project> projects =
                _context.Projects
                        .Include(p => p.Tags)
                        .Include(p => p.TopProject)
                        .Where(p => p.TopProject != null)
                        .OrderBy(p => p.TopProject!.Rank);

            if (limit is not null)
                projects = projects.Take(Convert.ToInt32(limit));

            return await projects.ToArrayAsync();
        }

        public async Task<int[]> MarkAsync(int[] ids)
        {
            int CountOfTopProjects = await _context.TopProjects.CountAsync();
            int MaxRankInDb = CountOfTopProjects > 0
                ? await _context.TopProjects.MaxAsync(p => p.Rank)
                : 0;

            if (CountOfTopProjects + ids.Length > MaxTopProjectsCount)
                throw new Exception($@"Max limit of Top projects has achieved, please remove {CountOfTopProjects + ids.Length - MaxTopProjectsCount} projects and try again");

            List<int> res = new List<int>();

            foreach (int id in ids)
            {
                Project? project = await GetProjectByIdIncludeingTopProject(id);

                if (project is null || project.TopProject is not null) continue;

                await _context.TopProjects.AddAsync(new TopProject
                {
                    ProjectId = id,
                    Rank = ++MaxRankInDb
                });

                res.Add(id);
            }

            return res.ToArray();
        }

        public async Task<bool> RemoveAsync(int id)
        {
            Project? project = await GetProjectByIdIncludeingTopProject(id);

            if (project is null || project.TopProject is null)
                return false;

            _context.TopProjects.Remove(project.TopProject);

            IQueryable<TopProject> topProjects =
                _context.TopProjects
                        .Where(t => t.Rank > project.TopProject.Rank);

            await topProjects.ForEachAsync(t => t.Rank--);

            _context.TopProjects.UpdateRange(topProjects);

            return true;
        }

        private async Task<Project?> GetProjectByIdIncludeingTopProject(int id)
            => await _context.Projects
                             .Include(p => p.TopProject)
                             .FirstOrDefaultAsync(p => p.Id == id);
    }
}

