using Authentication.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddApplicationConfiguration();

builder.Services.ConfigureForwardedHeaders();

builder.Services.ConfigureMapping();

builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.ConfigureRepositoryManager();

builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.ConfigureAuthorization();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
    app.UseForwardedHeaders();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

await app.RunAsync();
