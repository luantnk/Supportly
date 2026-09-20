using Microsoft.OpenApi;

namespace Supportly.API.Extensions;

/// <summary>
/// Extension methods for registering and configuring Swagger/OpenAPI documentation.
/// </summary>
public static class SwaggerExtensions
{
    /// <summary>
    /// Registers the Swagger generator and configures the API documentation,
    /// including XML comments and annotation support.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the Swagger services to.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance so that calls can be chained.</returns>
    public static IServiceCollection AddSwaggerDocs(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Supportly API",
                Version = "v1",
                Description = "API for managing support incidents"
            });

            var xmlPath = Path.Combine(
                AppContext.BaseDirectory,
                $"{typeof(Program).Assembly.GetName().Name}.xml");
            if (File.Exists(xmlPath))
                options.IncludeXmlComments(xmlPath);

            options.EnableAnnotations();
        });
        return services;
    }

    /// <summary>
    /// Enables the Swagger middleware and Swagger UI. Only active when the application
    /// is running in the Development environment.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> to configure.</param>
    /// <returns>The same <see cref="WebApplication"/> instance so that calls can be chained.</returns>
    public static WebApplication UseSwaggerDocs(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint("/swagger/v1/swagger.json", "Supportly API v1");
                o.DocumentTitle = "Supportly API Documentation";
            });
        }
        return app;
    }
}