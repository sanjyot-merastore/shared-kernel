using System.Security.Cryptography;
using System.Text;
using MeraStore.Shared.Kernel.Caching.Helper;
using MeraStore.Shared.Kernel.Caching.Interfaces;
using MeraStore.Shared.Kernel.Caching.Strategy;
using Microsoft.AspNetCore.Http;

namespace MeraStore.Shared.Kernel.Caching.Middlewares;

/// <summary>
/// Middleware that enforces idempotency by rejecting duplicate POST, PUT, or PATCH requests.
/// Uses an <see cref="ICacheProvider"/> (e.g., Redis) to track processed idempotency keys.
/// If the same request is received again with the same key, it returns 409 Conflict.
/// </summary>
public class IdempotencyMiddleware(RequestDelegate next, ICacheProviderFactory cacheProvider, CacheStrategy strategy)
{
    private const string IdempotencyHeader = "Idempotency-Key";

    /// <summary>
    /// The underlying cache provider used to store idempotency keys.
    /// </summary>
    private readonly ICacheProvider _cache = cacheProvider.Get(strategy);

    /// <summary>
    /// Intercepts HTTP requests, checks for the Idempotency-Key header,
    /// and prevents reprocessing of duplicate requests within a fixed time window.
    /// </summary>
    /// <param name="context">The HTTP context for the current request.</param>
    public async Task InvokeAsync(HttpContext context)
    {
        var method = context.Request.Method.ToUpperInvariant();

        // Only apply to write methods
        if (method != "POST" && method != "PUT" && method != "PATCH")
        {
            await next(context);
            return;
        }

        // Require idempotency key
        if (!context.Request.Headers.TryGetValue(IdempotencyHeader, out var key) || string.IsNullOrWhiteSpace(key))
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("Missing Idempotency-Key header");
            return;
        }

        var cacheKey = GenerateCacheKey(context.Request.Path, key!);

        // Check for existing key in cache
        if (await _cache.ExistsAsync(cacheKey))
        {
            context.Response.StatusCode = StatusCodes.Status409Conflict;
            await context.Response.WriteAsync("Duplicate request blocked by idempotency middleware.");
            return;
        }

        // Capture response for possible future enhancements (e.g., replays)
        var originalBody = context.Response.Body;
        using var memStream = new MemoryStream();
        context.Response.Body = memStream;

        await next(context);

        memStream.Seek(0, SeekOrigin.Begin);
        var responseBody = await new StreamReader(memStream).ReadToEndAsync();

        // Store key to prevent duplicates (only storing flag, not response)
        await _cache.SetAsync(
            cacheKey,
            value: true,
            options: new()
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(5)
            });

        memStream.Seek(0, SeekOrigin.Begin);
        await memStream.CopyToAsync(originalBody);
        context.Response.Body = originalBody;
    }

    /// <summary>
    /// Generates a unique cache key based on the request path and the provided idempotency key.
    /// </summary>
    /// <param name="path">The request path (e.g., /api/orders).</param>
    /// <param name="idempotencyKey">The value of the Idempotency-Key header.</param>
    /// <returns>A hashed, namespaced cache key string.</returns>
    private static string GenerateCacheKey(string path, string idempotencyKey)
    {
        using var sha256 = SHA256.Create();
        var raw = $"{path}:{idempotencyKey}";
        var bytes = Encoding.UTF8.GetBytes(raw);
        var hash = sha256.ComputeHash(bytes);
        return $"idemp:{Convert.ToHexString(hash)}";
    }
}
