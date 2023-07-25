﻿using IdentityServer;
using IdentityServer.Database;
using IdentityServer.Extensions;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Services.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDefaultCors(builder.Configuration);

builder.Services.AddDefaultApiVersioning();

builder.Services.AddControllers();

string connectionString = builder.Configuration.GetConnectionString("IdentityDB")!;
var migrationsAssembly = typeof(Config).Assembly.GetName().Name;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly)));

builder.Services.AddDefaultIdentity();
builder.Services.AddDefaultIdentityServer(builder.Configuration, connectionString, migrationsAssembly);

builder.Services.AddAuthorization();

builder.Services.AddSerilog();
builder.Host.UseSerilog((ctx, lc) => lc
.WriteTo.Console());

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseCors();

app.UseHttpsRedirection();

app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });
app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();

app.MapControllers();

app.UseSerilogRequestLogging();

await SeedData.EnsureSeedData(app);

await app.RunAsync();
