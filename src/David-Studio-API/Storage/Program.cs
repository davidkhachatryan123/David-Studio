using Microsoft.Extensions.FileProviders;
using Portfolio.Extensions;
using Services.Common;
using Services.Common.Configurations;
using Storage.Services;
using Storage.Grpc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDefaultApiVersioning();

builder.Services.AddControllers();
builder.Services.ConfigureOptions<FormOptionsConfiguration>();

builder.Services.AddGrpc();

builder.Services.ConfigureDb(builder.Configuration);
builder.Services.ConfigureMapping();

builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

builder.Services.ConfigureMessageBus();

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
app.MapGrpcService<StorageService>();

app.MigrateDatabase();

await app.RunAsync();
