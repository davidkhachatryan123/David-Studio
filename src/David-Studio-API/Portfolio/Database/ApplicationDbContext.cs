using System.Collections.Generic;
using System.Reflection.Emit;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore;
using Portfolio.Models;

namespace Portfolio.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Tag> Tags { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => base.OnModelCreating(modelBuilder);
    }
}

