using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Get<string[]>()!);
    }));

var identityUrl = builder.Configuration.GetValue<string>("IdentityUrl");

if (identityUrl is not null)
    builder.Services.AddAuthentication()
        .AddJwtBearer("IdentityApiKey", x =>
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

app.UseCors();

app.UseOcelot().Wait();

app.Run();
