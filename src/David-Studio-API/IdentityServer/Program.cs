using Duende.IdentityServer.Configuration;
using IdentityServer;
using IdentityServer.Database;
using IdentityServer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("IdentityDB")!;
var migrationsAssembly = typeof(Config).Assembly.GetName().Name;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly)));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
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

builder.Services.AddIdentityServer(options =>
{
    options.IssuerUri = "null";

    options.Events.RaiseSuccessEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;

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

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseRouting();
app.UseAuthorization();
app.UseIdentityServer();

using (var scope = app.Services.CreateScope())
{
    await SeedData.EnsureSeedData(scope, app.Configuration, app.Logger);
}

await app.RunAsync();
