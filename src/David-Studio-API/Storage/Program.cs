
using Microsoft.Extensions.FileProviders;
using Portfolio.Extensions;
using Services.Common;
using Storage.Configurations;
using Storage.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDefaultApiVersioning();

builder.Services.AddControllers();

builder.Services.ConfigureOptions<FormOptionsConfiguration>();

builder.Services.ConfigureDb(builder.Configuration);

builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

builder.Services.AddDefaultSwagger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDefaultSwagger();

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
    RequestPath = new PathString("/Resources")
});

app.MapControllers();

app.MigrateDatabase();

await app.RunAsync();
