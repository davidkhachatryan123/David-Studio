using IdentityServer;
using IdentityServer.Database;
using IdentityServer.Extensions;
using IdentityServer.Grpc.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Services.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDefaultCors(builder.Configuration);

builder.Services.AddDefaultApiVersioning();

builder.Services.AddControllers();

builder.Services.AddEventBus(builder.Configuration);

builder.Services.AddGrpc();

builder.Services.ConfigureMapping();

string connectionString = builder.Configuration.GetConnectionString("IdentityDB")!;
var migrationsAssembly = typeof(Config).Assembly.GetName().Name;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly)));

builder.Services.AddDefaultIdentity();
builder.Services.AddDefaultIdentityServer(builder.Configuration, connectionString, migrationsAssembly);

builder.Services.AddAuthorization();

builder.Services.AddDefaultSwagger();

builder.Services.AddSerilog();
builder.Host.UseSerilog((ctx, lc) => lc
.WriteTo.Console());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseDefaultSwagger();
}

app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });
app.UseCors();

app.UseHsts();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.MapGrpcService<AdminsService>();

app.UseSerilogRequestLogging();

await SeedData.EnsureSeedData(app);

await app.RunAsync();
