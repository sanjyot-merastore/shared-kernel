using MeraStore.Shared.Kernel.Common.Core;
using MeraStore.Shared.Kernel.WebApi.Attributes;

using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace MeraStore.Shared.Kernel.WebApi.Filters
{
    /// <summary>
    /// Adds custom headers to Swagger UI using the <see cref="HeaderAttribute"/>.
    /// Supports both MVC and Minimal APIs.
    /// </summary>
    public class HeadersFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            // 🧩 Add default Correlation ID if missing
            if (!operation.Parameters.Any(p =>
                    p.Name == Constants.Headers.CorrelationId &&
                    p.In == ParameterLocation.Header))
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = Constants.Headers.CorrelationId,
                    In = ParameterLocation.Header,
                    Required = false,
                    Description = "Correlation ID for tracking requests across services.",
                    Schema = new OpenApiSchema { Type = "string" }
                });
            }

            // 🎯 Get HeaderAttributes from controller/method (MVC)
            var methodLevelHeaders = context.MethodInfo
                .GetCustomAttributes(typeof(HeaderAttribute), true)
                .Cast<HeaderAttribute>();

            // 🎯 Get HeaderAttributes from endpoint metadata (Minimal API)
            var endpointMetadataHeaders = context.ApiDescription.ActionDescriptor.EndpointMetadata
                .OfType<HeaderAttribute>();

            // 🍷 Combine and deduplicate
            var headerAttributes = methodLevelHeaders
                .Concat(endpointMetadataHeaders)
                .GroupBy(h => h.Name)
                .Select(g => g.First())
                .ToList();

            foreach (var header in headerAttributes)
            {
                // Skip if already present (e.g., manually added elsewhere)
                if (operation.Parameters.Any(p => p.Name == header.Name && p.In == ParameterLocation.Header))
                    continue;

                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = header.Name,
                    In = ParameterLocation.Header,
                    Required = header.Required,
                    Description = header.Description ?? $"{header.Name} header",
                    Schema = new OpenApiSchema { Type = "string" }
                });
            }
        }
    }
}
