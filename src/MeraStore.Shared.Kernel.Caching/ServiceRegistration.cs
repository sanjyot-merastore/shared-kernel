using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using MeraStore.Shared.Kernel.Caching.Providers;
using MeraStore.Shared.Kernel.Caching.Strategy;

namespace MeraStore.Shared.Kernel.Caching.Extensions
{
  public static class CachingServiceExtensions
  {
    /// <summary>
    /// Registers caching infrastructure. Always includes In-Memory.
    /// Optionally sets up Redis caching (not wired as ICacheProvider yet).
    /// </summary>
    public static IServiceCollection AddMeraStoreCaching(this IServiceCollection services, string? redisConnectionString = null)
    {

      services.AddSingleton<ICacheProviderFactory, CacheProviderFactory>();
      // Register in-memory cache infra + provider
      services.AddMemoryCache();
      services.AddSingleton<InMemoryCacheProvider>();

      // Optional Redis infra
      if (string.IsNullOrWhiteSpace(redisConnectionString)) return services;
      var multiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
      services.AddSingleton<IConnectionMultiplexer>(multiplexer);
      services.AddSingleton<RedisCacheProvider>();

      return services;
    }

  }
}