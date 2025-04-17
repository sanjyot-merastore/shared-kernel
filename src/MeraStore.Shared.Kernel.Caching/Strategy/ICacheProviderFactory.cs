using MeraStore.Shared.Kernel.Caching.Helper;
using MeraStore.Shared.Kernel.Caching.Interfaces;

namespace MeraStore.Shared.Kernel.Caching.Strategy;

public interface ICacheProviderFactory
{
  ICacheProvider Get(CacheStrategy strategy);
}

public class CacheProviderFactory(IEnumerable<ICacheProvider> providers) : ICacheProviderFactory
{
  private readonly Dictionary<CacheStrategy, ICacheProvider> _providerMap =
    providers.ToDictionary(p => p.Strategy);

  public ICacheProvider Get(CacheStrategy strategy)
  {
    return _providerMap.TryGetValue(strategy, out var provider)
      ? provider
      : throw new NotSupportedException($"No cache provider found for strategy {strategy}");
  }
}
