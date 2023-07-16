using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Portfolio.Extensions;
using Portfolio.Grpc;
using Portfolio.Services;
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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDefaultSwagger();
}

app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase();

await app.RunAsync();