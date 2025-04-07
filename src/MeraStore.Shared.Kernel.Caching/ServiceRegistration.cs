using MeraStore.Shared.Kernel.Caching.Providers;
using MeraStore.Shared.Kernel.Caching.Strategy;
using MeraStore.Shared.Kernel.Caching.Interfaces;

namespace MeraStore.Shared.Kernel.Caching.Extensions;

[ExcludeFromCodeCoverage]
public static class CachingServiceCollectionExtensions
{
  public static IServiceCollection AddMeraStoreCaching(this IServiceCollection services, string? redisConnectionString = null,
    Action<CacheEntryOptions>? configureDefaults = null)
  {
    var defaultOptions = new CacheEntryOptions
    {
      AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
    };
    configureDefaults?.Invoke(defaultOptions);

    services.AddSingleton(defaultOptions);
    services.AddMemoryCache();
    services.AddSingleton<InMemoryCacheProvider>();

    if (!string.IsNullOrWhiteSpace(redisConnectionString))
    {
      var multiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
      services.AddSingleton<IConnectionMultiplexer>(multiplexer);
      services.AddSingleton<RedisCacheProvider>();
    }

    services.AddSingleton<ICacheProviderFactory, CacheProviderFactory>();

    return services;
  }
}