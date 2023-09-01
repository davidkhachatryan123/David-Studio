using AutoMapper;
using Duende.IdentityServer.Configuration;
using IdentityServer.Database;
using IdentityServer.Mappings;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace IdentityServer.Extensions
{
    public static class Extensions
    {
        public static void AddDefaultIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;

                // SignIn settings.
                options.SignIn.RequireConfirmedEmail = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789;@$!%*?&=#";
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
        }

        public static void AddDefaultIdentityServer(this IServiceCollection services, IConfiguration configuration, string connectionString, string? migrationsAssembly)
        {
            services.AddIdentityServer(options =>
            {
                options.IssuerUri = configuration["IdentityServer"]!;

                options.Events.RaiseSuccessEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;

                options.UserInteraction.LoginUrl = $"{configuration["IdentityWebSpa"]}/account/login";
                options.UserInteraction.LoginReturnUrlParameter = "returnUrl";
                options.UserInteraction.LogoutUrl = $"{configuration["IdentityWebSpa"]}/account/logout";
                options.UserInteraction.LogoutIdParameter = "logoutId";

                // new key every 30 days
                options.KeyManagement.RotationInterval = TimeSpan.FromDays(30);
                // announce new key 2 days in advance in discovery
                options.KeyManagement.PropagationTime = TimeSpan.FromDays(2);
                // keep old key for 7 days in discovery for validation of tokens
                options.KeyManagement.RetentionDuration = TimeSpan.FromDays(7);
                // don't delete keys after their retention period is over
                options.KeyManagement.DeleteRetiredKeys = false;
                options.KeyManagement.SigningAlgorithms = new[]
                {
                    // RS256 for older clients (with additional X.509 wrapping)
                    new SigningAlgorithmOptions(SecurityAlgorithms.RsaSha256) { UseX509Certificate = true },
                    // PS256
                    new SigningAlgorithmOptions(SecurityAlgorithms.RsaSsaPssSha256),
                    // ES256
                    new SigningAlgorithmOptions(SecurityAlgorithms.EcdsaSha256)
                };
            })
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = builder =>
                    builder.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = builder =>
                    builder.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly));

                // this enables automatic token cleanup. this is optional.
                options.EnableTokenCleanup = true;
                // interval in seconds (default is 3600)
                options.TokenCleanupInterval = 3600;
            })
            .AddAspNetIdentity<ApplicationUser>();
        }

        public static void ConfigureMapping(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(map =>
            {
                map.AddProfile<UsersMappingProfile>();
            });

            services.AddSingleton(mapperConfig.CreateMapper());
        }
    }
}
