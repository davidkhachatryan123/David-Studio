using EventBus.Abstractions;
using Messanger.Extensions;
using Messanger.IntegrationEvents.Events;
using Messanger.IntegrationEvents.Handlers;
using Messanger.Options;
using Serilog;
using Services.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<SmtpOptions>(
    builder.Configuration.GetSection(nameof(SmtpOptions)));

builder.Services.AddEventBus(builder.Configuration);
builder.Services.AddTransient<IIntegrationEventHandler<SendEmailIntegrationEvent>, SendEmailIntegrationEventHandler>();

builder.Services.AddSerilog();
builder.Host.UseSerilog((ctx, lc) => lc
.WriteTo.Console());

var app = builder.Build();

app.ConfigureEventBus();

await app.RunAsync();
