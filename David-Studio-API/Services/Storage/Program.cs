using Portfolio.Extensions;
using Services.Common.Configurations;
using Storage.Services;
using Storage.Grpc;
using Storage.Options;
using Services.Common.Extensions;
using EventBus.Abstractions;
using Storage.IntegrationEvents.Handlers;
using Storage.IntegrationEvents.Events;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStorageCors(builder.Configuration);

builder.Services.Configure<StorageOptions>(
    builder.Configuration.GetSection(nameof(StorageOptions)));

builder.Services.AddDefaultAuthentication(builder.Configuration);

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

builder.Services.AddHealthChecks();

builder.Services.AddSerilog();
builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console());

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDefaultSwagger(builder.Configuration);

app.UseCors("Resources");

app.UseStaticFilesDefaults();

app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.ConfigureEventBus();
app.MapGrpcService<StorageService>();

app.MapHealthChecks("/healthz");

app.UseSerilogRequestLogging();

app.MigrateDatabase();

await app.RunAsync();
