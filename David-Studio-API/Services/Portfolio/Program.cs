using Portfolio.Extensions;
using Portfolio.Grpc;
using Portfolio.Services;
using Serilog;
using Services.Common.Configurations;
using Services.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDefaultAuthentication(builder.Configuration);

builder.Services.AddDefaultApiVersioning();

builder.Services.AddControllers();

builder.Services.ConfigureOptions<FormOptionsConfiguration>();

builder.Services.ConfigureDb(builder.Configuration);
builder.Services.ConfigureMapping();

builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

builder.Services.AddEventBus(builder.Configuration);

builder.Services.AddScoped<IStorageDataClient, StorageDataClient>();

builder.Services.AddDefaultSwagger();

builder.Services.AddHealthChecks();

builder.Services.AddSerilog();
builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console());

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDefaultSwagger(builder.Configuration);

app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.MapHealthChecks("/healthz");

app.UseSerilogRequestLogging();

app.MigrateDatabase();

await app.RunAsync();
