using Serilog;
using Services.Common.Extensions;
using Users.Extensions;
using Users.Grpc.Clients;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDefaultAuthentication(builder.Configuration);

builder.Services.AddDefaultApiVersioning();

builder.Services.AddControllers();

builder.Services.ConfigureMapping();

builder.Services.AddEventBus(builder.Configuration);

builder.Services.AddScoped<IManageUsersClient, ManageUsersClient>();
builder.Services.AddScoped<IAdminsDataClient, AdminsDataClient>();

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

await app.RunAsync();