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
                        .OrderByDescending(p => p.TopProject!.Rank);

            if (limit is not null)
                projects = projects.Take(Convert.ToInt32(limit));

            return await projects.ToArrayAsync();
        }

        public async Task<IEnumerable<TopProject>> MarkAsync(int[] ids)
        {
            int CountOfTopProjects = await _context.TopProjects.CountAsync();
            int MaxRankInDb = CountOfTopProjects > 0
                ? await _context.TopProjects.MaxAsync(p => p.Rank)
                : 0;

            if (CountOfTopProjects + ids.Length > MaxTopProjectsCount)
                throw new Exception($@"Max limit of Top projects has achieved, please remove {CountOfTopProjects + ids.Length - MaxTopProjectsCount} projects and try again");

            List<TopProject> res = new();

            foreach (int id in ids)
            {
                Project? project = await GetProjectIncludingTopProject(id);

                if (project is null || project.TopProject is not null) continue;

                res.Add(new TopProject
                {
                    ProjectId = id,
                    Rank = ++MaxRankInDb
                });

                await _context.TopProjects.AddAsync(res.Last());
            }

            return res;
        }

        public async Task<IEnumerable<TopProject>> Reorder(int[] projectIds)
        {
            if (projectIds.Length != await _context.TopProjects.CountAsync())
                throw new Exception("Provided project ids length has not equivalent with db top projects count");
            if (projectIds.Length != projectIds.Distinct().Count())
                throw new Exception("Array of projects is must have contains unique indexes");

            List<TopProject> res = new();

            for (int i = 0; i < projectIds.Length; i++)
            {
                TopProject? topProject =
                    await _context.TopProjects.FirstOrDefaultAsync(
                        t => t.ProjectId == projectIds[i])
                    ?? throw new Exception("Provided project ids has invalid items");

                topProject.Rank = projectIds.Length - i;

                _context.TopProjects.Update(topProject);

                res.Add(topProject);
            }

            return res;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            Project? project = await GetProjectIncludingTopProject(id);

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

        private async Task<Project?> GetProjectIncludingTopProject(int id)
            => await _context.Projects
                             .Include(p => p.TopProject)
                             .FirstOrDefaultAsync(p => p.Id == id);
    }
}
