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

        public async Task<int[]> MarkAsTop(int[] ids)
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
                Project? project =
                    await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
                if (project is null) continue;

                TopProject? topProject =
                    await _context.TopProjects.FirstOrDefaultAsync(t => t.ProjectId == id);
                if (topProject is not null) continue;

                await _context.TopProjects.AddAsync(new TopProject
                {
                    ProjectId = id,
                    Rank = ++MaxRankInDb
                });

                res.Add(id);
            }

            return res.ToArray();
        }
    }
}

