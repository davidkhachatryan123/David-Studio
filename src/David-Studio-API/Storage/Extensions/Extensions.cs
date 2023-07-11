using System;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Portfolio.Database;
using Portfolio.Mappings;
using Storage;
using Storage.MessageBus;
using Storage.MessageBus.Services;
using Storage.Options;

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
            services.AddSingleton(provider => new MapperConfiguration(
                map =>
                {
                    map.AddProfile(new FilesMappingProfile(provider.GetService<IConfiguration>()!));
                })
            .CreateMapper());
        }

        public static void ConfigureMessageBus(this IServiceCollection services)
        {
            services.AddSingleton<IMessageBusClient, MessageBusClient>();

            services.AddHostedService<StorageImagesSubscriber>();
        }

        public static void UseStaticFilesDefaults(this WebApplication app)
        {
            StorageOptions storageOptions =
                app.Services.GetRequiredService<IOptions<StorageOptions>>().Value;

            StorageSetup.CreateDirIfNotExists(Path.Combine(
                    storageOptions.StoragePath,
                    storageOptions.ImagesDir));

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(storageOptions.StoragePath),
                RequestPath = new PathString("/Resources")
            });
        }
    }
}

