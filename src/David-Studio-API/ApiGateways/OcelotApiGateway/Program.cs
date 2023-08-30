using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

var identityUrl = builder.Configuration.GetValue<string>("IdentityUrl");
var authenticationProviderKey = "IdentityApiKey";

builder.Services.AddAuthentication()
    .AddJwtBearer(authenticationProviderKey, x =>
    {
        x.Authority = identityUrl;
        x.RequireHttpsMetadata = false;
        x.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidAudiences = new[] { "users", "portfolio", "pricing", "storage", "messenger" }
        };
    });

IConfiguration configuration = builder.Configuration
    .AddJsonFile(Path.Combine("configuration", "configuration.json"), false, true)
    .Build();

builder.Services.AddOcelot(configuration);

var app = builder.Build();

app.UseOcelot().Wait();

app.Run();
