using MeraStore.Shared.Kernel.Caching.Extensions.Helper;
using MeraStore.Shared.Kernel.Caching.Interfaces;

using Microsoft.Extensions.Caching.Memory;

namespace MeraStore.Shared.Kernel.Caching.Providers;

public class InMemoryCacheProvider(IMemoryCache cache, CacheEntryOptions defaultOptions) : ICacheProvider
{
  public Task<T?> GetAsync<T>(string key)
  {
    cache.TryGetValue(key, out T? value);
    return Task.FromResult(value);
  }

  public Task SetAsync<T>(string key, T value, CacheEntryOptions? options = null)
  {
    var effectiveOptions = options ?? defaultOptions;
    var memoryOptions = CacheOptionsConverter.ToMemoryOptions(effectiveOptions);

    cache.Set(key, value, memoryOptions);
    return Task.CompletedTask;
  }

  public Task<bool> RemoveAsync(string key)
  {
    cache.Remove(key);
    return Task.FromResult(true);
  }

  public Task<bool> ExistsAsync(string key)
  {
    return Task.FromResult(cache.TryGetValue(key, out _));
  }
}

