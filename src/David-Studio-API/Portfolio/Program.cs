using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Portfolio.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddApiVersioning();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
