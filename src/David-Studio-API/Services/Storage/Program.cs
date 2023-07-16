using Microsoft.Extensions.FileProviders;
using Portfolio.Extensions;
using Services.Common;
using Services.Common.Configurations;
using Storage.Services;
using Storage.Grpc;
using Storage.Options;
using Services.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<StorageOptions>(
    builder.Configuration.GetSection(nameof(StorageOptions)));

builder.Services.AddDefaultApiVersioning();

builder.Services.AddControllers();
builder.Services.ConfigureOptions<FormOptionsConfiguration>();

builder.Services.AddGrpc();

builder.Services.ConfigureDb(builder.Configuration);
builder.Services.ConfigureMapping();

builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

builder.Services.AddEventBus(builder.Configuration);

builder.Services.AddDefaultSwagger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDefaultSwagger();

app.UseStaticFilesDefaults();

app.MapControllers();

app.ConfigureEventBus();
app.MapGrpcService<StorageService>();

app.MigrateDatabase();

await app.RunAsync();
