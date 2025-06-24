using Microsoft.AspNetCore.Routing;

namespace MeraStore.Shared.Kernel.WebApi.Interfaces;

/// <summary>
/// Represents a contract for defining and mapping HTTP endpoints in a minimal API architecture.
/// </summary>
public interface IEndpoint
{
    /// <summary>
    /// Configures and maps the implementing class's endpoints to the specified route builder.
    /// This method is typically called during application startup to register routes dynamically.
    /// </summary>
    /// <param name="app">The <see cref="IEndpointRouteBuilder"/> used to define and register routes.</param>
    void MapEndpoints(IEndpointRouteBuilder app);
}
