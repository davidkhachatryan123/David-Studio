using Microsoft.Extensions.FileProviders;
using Portfolio.Extensions;
using Services.Common;
using Services.Common.Configurations;
using Storage.Services;
using Storage.Grpc;
using Storage.Options;
using Services.Common.Extensions;
using Storage.IntegrationEvents;
using EventBus.Abstractions;
using Storage.IntegrationEvents.Handlers;
using Storage.IntegrationEvents.Events;
using Serilog;

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
builder.Services.AddTransient<IIntegrationEventHandler<ImagesDeleteIntegrationEvent>, ImagesDeleteIntegrationEventHandler>();

builder.Services.AddDefaultSwagger();

builder.Services.AddSerilog();
builder.Host.UseSerilog((ctx, lc) => lc
.WriteTo.Console());

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDefaultSwagger();

app.UseStaticFilesDefaults();

app.MapControllers();

app.ConfigureEventBus();
app.MapGrpcService<StorageService>();

app.UseSerilogRequestLogging();

app.MigrateDatabase();

await app.RunAsync();
