using IdentityServer;
using IdentityServer.Database;
using IdentityServer.Extensions;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("IdentityDB")!;
var migrationsAssembly = typeof(Config).Assembly.GetName().Name;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly)));

builder.Services.AddDefaultIdentity();

builder.Services.AddDefaultIdentityServer(connectionString, migrationsAssembly);

builder.Services.AddAuthorization();

builder.Services.AddSerilog();
builder.Host.UseSerilog((ctx, lc) => lc
.WriteTo.Console());

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseRouting();

app.UseAuthorization();
app.UseIdentityServer();

app.UseSerilogRequestLogging();

await SeedData.EnsureSeedData(app);

await app.RunAsync();
