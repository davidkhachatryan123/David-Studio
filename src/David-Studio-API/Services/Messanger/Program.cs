using Messanger.Extensions;
using Messanger.Services;
using Serilog;
using Services.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureOptions(builder.Configuration);

builder.Services.AddSingleton<IEmailService, EmailService>();

builder.Services.AddEventBus(builder.Configuration);
builder.Services.AddEventBusHandlers();

builder.Services.AddSerilog();
builder.Host.UseSerilog((ctx, lc) => lc
.WriteTo.Console());

var app = builder.Build();

app.ConfigureEventBus();

await app.RunAsync();
