using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Portfolio.Extensions;
using Portfolio.Grpc;
using Portfolio.Services;
using Serilog;
using Services.Common;
using Services.Common.Configurations;
using Services.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDefaultApiVersioning();

builder.Services.AddControllers();

builder.Services.ConfigureOptions<FormOptionsConfiguration>();

builder.Services.ConfigureDb(builder.Configuration);
builder.Services.ConfigureMapping();

builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

builder.Services.AddEventBus(builder.Configuration);

builder.Services.AddScoped<IStorageDataClient, StorageDataClient>();

builder.Services.AddDefaultSwagger();

builder.Services.AddSerilog();
builder.Host.UseSerilog((ctx, lc) => lc
.WriteTo.Console());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDefaultSwagger();
}

app.UseAuthorization();

app.MapControllers();

app.UseSerilogRequestLogging();

app.MigrateDatabase();

await app.RunAsync();
