using System;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Portfolio.Database;
using Portfolio.Mappings;

namespace Portfolio.Extensions
{
    public static class Extensions
    {
        public static void ConfigureDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("StorageDb"),
                x => x.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name)));
        }

        public static void MigrateDatabase(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.Migrate();
            }
        }

        public static void ConfigureMapping(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(map =>
            {
                map.AddProfile<FilesMappingProfile>();
            });

            services.AddSingleton(mapperConfig.CreateMapper());
        }
    }
}

