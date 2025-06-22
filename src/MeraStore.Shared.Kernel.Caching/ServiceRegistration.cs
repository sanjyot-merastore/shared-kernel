using MeraStore.Shared.Kernel.Caching.Providers;
using MeraStore.Shared.Kernel.Caching.Strategy;
using MeraStore.Shared.Kernel.Caching.Helper;
using MeraStore.Shared.Kernel.Caching.Interfaces;

namespace MeraStore.Shared.Kernel.Caching.Extensions;

[ExcludeFromCodeCoverage]
public static class ServiceRegistration
{
    /// <summary>
    /// Adds MeraStore Caching services with configurable caching strategies.
    /// </summary>
    /// <param name="services">The collection of services to register.</param>
    /// <param name="redisConnectionString">The connection string for Redis, optional.</param>
    /// <param name="configureDefaults">Action to configure default cache entry options.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddMeraStoreCaching(this IServiceCollection services, string? redisConnectionString = null,
        Action<CacheEntryOptions>? configureDefaults = null)
    {
        // Set up default cache entry options
        var defaultOptions = new CacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
        };
        configureDefaults?.Invoke(defaultOptions);

        services.AddSingleton(defaultOptions);
        services.AddMemoryCache();

        // Register Redis caching strategy if Redis connection string is provided
        if (!string.IsNullOrWhiteSpace(redisConnectionString))
        {
            var multiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
            services.AddSingleton<IConnectionMultiplexer>(multiplexer);
            services.AddSingleton<ICacheProvider, RedisCacheProvider>(); // Register Redis strategy
        }

        // Register InMemory caching strategy if Redis is not configured
        services.AddSingleton<ICacheProvider, InMemoryCacheProvider>(); // Register In-memory strategy

        // Factory for creating the appropriate caching provider
        services.AddSingleton<ICacheProviderFactory, CacheProviderFactory>();

        return services;
    }
}