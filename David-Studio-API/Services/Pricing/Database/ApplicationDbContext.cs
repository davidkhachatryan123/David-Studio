using Microsoft.EntityFrameworkCore;
using Pricing.Models;

namespace Messanger.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ServicesPricing> ServicesPricings { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}

