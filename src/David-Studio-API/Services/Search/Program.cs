using Search;
using Search.Extensions;
using Serilog;
using Services.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDefaultApiVersioning();

builder.Services.AddControllers();

builder.Services.AddElasticSearch(builder.Configuration);

builder.Services.AddEventBus(builder.Configuration);
//builder.Services.AddEventBusHandlers();

builder.Services.AddDefaultSwagger();

builder.Services.AddSerilog();
builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console());

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDefaultSwagger(builder.Configuration);

//app.ConfigureEventBus();

app.MapControllers();

app.UseSerilogRequestLogging();

await SeedData.SeedIndices(app);

await app.RunAsync();
