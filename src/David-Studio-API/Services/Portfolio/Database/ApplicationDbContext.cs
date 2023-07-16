using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Portfolio.Models;

namespace Portfolio.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProjectTag> ProjectTags { get; set; }

        public DbSet<TopProject> TopProjects { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Projects)
                .UsingEntity<ProjectTag>();

            modelBuilder.Entity<TopProject>()
                .HasOne(e => e.Project)
                .WithOne(e => e.TopProject)
                .HasForeignKey<TopProject>(e => e.ProjectId);

            base.OnModelCreating(modelBuilder);
        }
    }
}

