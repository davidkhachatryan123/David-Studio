using Search;
using Search.Extensions;
using Search.Services;
using Search.Services.RepositoryManager;
using Serilog;
using Services.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDefaultApiVersioning();

builder.Services.AddControllers();

builder.Services.AddMappings();

builder.Services.AddElasticSearch(builder.Configuration);

builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<ISearchService, SearchService>();

builder.Services.AddEventBus(builder.Configuration);
builder.Services.AddEventBusHandlers();

builder.Services.AddDefaultSwagger();

builder.Services.AddHealthChecks();

builder.Services.AddSerilog();
builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console());

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDefaultSwagger(builder.Configuration);

app.AddEventBusSubscriptions();

app.MapControllers();

app.MapHealthChecks("/healthz");

app.UseSerilogRequestLogging();

await SeedData.SeedIndices(app);

await app.RunAsync();
