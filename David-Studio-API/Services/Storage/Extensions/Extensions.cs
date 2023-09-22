using System.Reflection;
using AutoMapper;
using EventBus.Abstractions;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Portfolio.Database;
using Portfolio.Mappings;
using Storage;
using Storage.IntegrationEvents.Events;
using Storage.Options;

namespace Portfolio.Extensions
{
    public static class Extensions
    {
        public static void AddStorageCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
                options.AddPolicy("Resources", policy => policy
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithOrigins(configuration["AllowedOrigins"]!.Split(";"))
                )
            );
        }

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

        public static void ConfigureEventBus(this WebApplication app)
        {
            var eventBus = app.Services.GetRequiredService<IEventBus>();

            eventBus.Subscribe<ImagesDeleteIntegrationEvent, IIntegrationEventHandler<ImagesDeleteIntegrationEvent>>();
        }

        public static void UseStaticFilesDefaults(this WebApplication app)
        {
            StorageOptions storageOptions =
                app.Services.GetRequiredService<IOptions<StorageOptions>>().Value;

            StorageSetup.CreateDirIfNotExists(Path.Combine(
                    storageOptions.StoragePath,
                    storageOptions.ImagesDir));

            ICorsService corsService = app.Services.GetRequiredService<ICorsService>();
            ICorsPolicyProvider corsPolicyProvider = app.Services.GetRequiredService<ICorsPolicyProvider>();

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(storageOptions.StoragePath),
                RequestPath = new PathString("/Resources"),
                OnPrepareResponse = (ctx) =>
                {
                    var policy = corsPolicyProvider.GetPolicyAsync(ctx.Context, "Resources")
                        .ConfigureAwait(false)
                        .GetAwaiter()
                        .GetResult();

                    var corsResult = corsService.EvaluatePolicy(ctx.Context, policy!);

                    corsService.ApplyResult(corsResult, ctx.Context.Response);
                }
            });
        }
    }
}
