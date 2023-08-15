using Serilog;
using Services.Common.Extensions;
using Users.Extensions;
using Users.Grpc.Clients;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDefaultApiVersioning();

builder.Services.AddControllers();

builder.Services.ConfigureMapping();

builder.Services.AddEventBus(builder.Configuration);

builder.Services.AddScoped<IUsersDataClient, UsersDataClient>();

builder.Services.AddDefaultSwagger();

builder.Services.AddSerilog();
builder.Host.UseSerilog((ctx, lc) => lc
.WriteTo.Console());

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDefaultSwagger();

app.UseAuthorization();

app.MapControllers();

app.UseSerilogRequestLogging();

await app.RunAsync();