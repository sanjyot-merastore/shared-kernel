using MeraStore.Shared.Kernel.Caching.Interfaces;
using MeraStore.Shared.Kernel.Caching.Providers;

namespace MeraStore.Shared.Kernel.Caching.Strategy;

public interface ICacheProviderFactory
{
  ICacheProvider Get(CacheStrategy strategy);
}

public class CacheProviderFactory(InMemoryCacheProvider inMemory, RedisCacheProvider redis) : ICacheProviderFactory
{
  public ICacheProvider Get(CacheStrategy strategy) => strategy switch
  {
    CacheStrategy.InMemory => inMemory,
    CacheStrategy.Redis => redis,
    _ => inMemory
  };
}

