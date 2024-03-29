﻿using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.WithOrigins(builder.Configuration.GetValue<string>("AllowedOrigins")!.Split(';'));
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

string? ConfigFileDirectory = builder.Configuration.GetValue<string>("ConfigFileDirectory");

IConfiguration configuration = builder.Configuration
    .AddJsonFile(Path.Combine(ConfigFileDirectory!, "configuration.json"), false, true)
    .Build();

builder.Services.AddHealthChecks();

builder.Services.AddOcelot(configuration);

var app = builder.Build();

app.UseRouting();

app.UseCors();

app.MapHealthChecks("/healthz");

app.UseEndpoints(e => e.MapControllers());

app.UseOcelot().Wait();

app.Run();
