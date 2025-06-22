using MeraStore.Shared.Kernel.Caching.Helper;
using MeraStore.Shared.Kernel.WebApi.Middleware;

using Microsoft.AspNetCore.Builder;

namespace MeraStore.Shared.Kernel.Caching.Middlewares;

public static class IdempotencyMiddlewareExtensions
{
    /// <summary>
    /// Adds MeraStore idempotency middleware to the request pipeline using the specified caching strategy.
    /// </summary>
    /// <param name="app">The application builder.</param>
    /// <param name="strategy">The caching strategy to use (e.g., Redis, Memory, Hybrid).</param>
    public static IApplicationBuilder UseMeraStoreIdempotency(this IApplicationBuilder app, CacheStrategy strategy = CacheStrategy.Redis)
    {
        return app.UseMiddleware<IdempotencyMiddleware>(strategy);
    }
}
