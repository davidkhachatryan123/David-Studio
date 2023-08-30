using Messanger.Extensions;
using Messanger.Services;
using Messanger.Services.RepositoryManager;
using Serilog;
using Services.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDefaultAuthentication(builder.Configuration);

builder.Services.AddDefaultApiVersioning();

builder.Services.AddControllers();

builder.Services.ConfigureOptions(builder.Configuration);

builder.Services.ConfigureDb(builder.Configuration);
builder.Services.ConfigureMapping();

builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

builder.Services.AddSingleton<IEmailService, EmailService>();

builder.Services.AddEventBus(builder.Configuration);
builder.Services.AddEventBusHandlers();

builder.Services.AddDefaultSwagger();

builder.Services.AddSerilog();
builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console());

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDefaultSwagger(builder.Configuration);

app.ConfigureEventBus();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.UseSerilogRequestLogging();

app.MigrateDatabase();

await app.RunAsync();
