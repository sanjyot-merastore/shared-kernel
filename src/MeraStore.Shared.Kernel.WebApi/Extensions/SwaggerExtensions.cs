using MeraStore.Shared.Kernel.WebApi.Filters;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace MeraStore.Shared.Kernel.WebApi.Extensions;

public static class SwaggerExtensions
{
    /// <summary>
    /// Adds Swagger support with a custom operation filter and XML comments.
    /// </summary>
    public static IServiceCollection AddCustomSwagger(
        this IServiceCollection services,
        string serviceName,
        string xmlFileName,
        string version = "v1", params Type[] operationFilterTypes)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(version, new OpenApiInfo
            {
                Title = $"{serviceName} API",
                Version = version,
                Description = "Auto-generated documentation for API endpoints."
            });

            TryAddXmlComments(c, xmlFileName);

            // 💡 Dynamically add all operation filters
            foreach (var filterType in operationFilterTypes.Distinct())
            {
                if (typeof(IOperationFilter).IsAssignableFrom(filterType))
                {
                    c.OperationFilterDescriptors.Add(new FilterDescriptor
                    {
                        Type = filterType
                    });
                }
            }

            AddJwtSecurityDefinition(c);
        });

        return services;
    }

    /// <summary>
    /// Adds Swagger support using the default HeadersFilter and XML comments.
    /// </summary>
    public static IServiceCollection AddCustomSwagger(
        this IServiceCollection services,
        string serviceName,
        string xmlFileName,
        string version = "v1")
    {
        return services.AddCustomSwagger(serviceName, xmlFileName, version, typeof(HeadersFilter));
    }

    /// <summary>
    /// Adds Swagger UI and JSON endpoints for a single API version.
    /// </summary>
    public static IApplicationBuilder UseCustomSwagger(
        this IApplicationBuilder app,
        string serviceName,
        string version = "v1")
    {
        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{serviceName} {version}");
            c.RoutePrefix = "swagger";
            c.DisplayRequestDuration(); // Show how long requests take
        });

        return app;
    }

    /// <summary>
    /// Adds Swagger UI support for multiple API versions.
    /// </summary>
    public static IApplicationBuilder UseCustomSwagger(
        this IApplicationBuilder app,
        string serviceName,
        params string[] versions)
    {
        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            foreach (var version in versions)
            {
                c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{serviceName} {version}");
            }

            c.RoutePrefix = "swagger";
            c.DisplayRequestDuration();
        });

        return app;
    }

    // 🛡️ Add Bearer JWT security definition
    private static void AddJwtSecurityDefinition(SwaggerGenOptions c)
    {
        var scheme = new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = "Enter 'Bearer {token}'",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT"
        };

        c.AddSecurityDefinition("Bearer", scheme);

        var requirement = new OpenApiSecurityRequirement
        {
            [scheme] = []
        };

        c.AddSecurityRequirement(requirement);
    }

    // 📄 Try including XML comments for better doc generation
    private static void TryAddXmlComments(SwaggerGenOptions c, string xmlFileName)
    {
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
        if (File.Exists(xmlPath))
        {
            c.IncludeXmlComments(xmlPath);
        }
    }
}