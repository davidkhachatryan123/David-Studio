using System;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using IdentityServer.Database;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using static System.Formats.Asn1.AsnWriter;

namespace IdentityServer
{
    public class SeedData
    {
        public static async Task EnsureSeedData(IServiceScope scope, IConfiguration configuration, ILogger logger)
        {
            var retryPolicy = CreateRetryPolicy(configuration, logger);

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var grantContext = scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();
            var configContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

            await retryPolicy.ExecuteAsync(async () =>
            {
                await context.Database.MigrateAsync();
                await grantContext.Database.MigrateAsync();
                await configContext.Database.MigrateAsync();

                var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                await EnsureSeedUsers(context, configuration, logger, userMgr);
                await EnsureSeedData(configContext, configuration);
            });
        }

        private static async Task EnsureSeedUsers(
            ApplicationDbContext context, IConfiguration configuration,
            ILogger logger, UserManager<ApplicationUser> userMgr)
        {
            var defaultUser = await userMgr.FindByNameAsync(configuration["DefaultUser:UserName"]!);

            if (defaultUser == null)
            {
                defaultUser = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = configuration["DefaultUser:UserName"],
                    Email = configuration["DefaultUser:Email"],
                    EmailConfirmed = true,

                };

                var result = userMgr.CreateAsync(defaultUser, configuration["DefaultUser:Password"]!).Result;

                if (!result.Succeeded)
                    throw new Exception(result.Errors.First().Description);

                logger.LogDebug("Default user created");
            }
            else
                logger.LogDebug("Default user already exists");
        }

        private static async Task EnsureSeedData(ConfigurationDbContext context, IConfiguration configuration)
        {
            if (!context.Clients.Any())
            {
                foreach (var client in Config.Clients(configuration))
                    await context.Clients.AddAsync(client.ToEntity());
                await context.SaveChangesAsync();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in Config.IdentityResources)
                    await context.IdentityResources.AddAsync(resource.ToEntity());
                await context.SaveChangesAsync();
            }

            if (!context.ApiResources.Any())
            {
                foreach (var apiResource in Config.ApiResource)
                    await context.ApiResources.AddAsync(apiResource.ToEntity());
                await context.SaveChangesAsync();
            }

            if (!context.ApiScopes.Any())
            {
                foreach (var apiScope in Config.ApiScopes)
                    await context.ApiScopes.AddAsync(apiScope.ToEntity());
                await context.SaveChangesAsync();
            }
        }

        private static AsyncPolicy CreateRetryPolicy(IConfiguration configuration, Microsoft.Extensions.Logging.ILogger logger)
        {
            var retryMigrations = false;
            bool.TryParse(configuration["RetryMigrations"], out retryMigrations);

            // Only use a retry policy if configured to do so.
            // When running in an orchestrator/K8s, it will take care of restarting failed services.
            if (retryMigrations)
            {
                return Policy.Handle<Exception>().
                    WaitAndRetryForeverAsync(
                        sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                        onRetry: (exception, retry, timeSpan) => logger.LogWarning(exception, "Error migrating database (retry attempt {retry})", retry));
            }

            return Policy.NoOpAsync();
        }
    }
}

