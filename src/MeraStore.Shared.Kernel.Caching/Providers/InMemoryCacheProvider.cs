using MeraStore.Shared.Kernel.Caching.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace MeraStore.Shared.Kernel.Caching.Providers;

public class InMemoryCacheProvider(IMemoryCache memoryCache) : ICacheProvider
{
  public Task<T?> GetAsync<T>(string key)
    => Task.FromResult(memoryCache.TryGetValue(key, out var value) ? (T?)value : default);

  public Task SetAsync<T>(string key, T value, CacheEntryOptions? options = null)
  {
    var entryOptions = new MemoryCacheEntryOptions();

    if (options?.AbsoluteExpirationRelativeToNow != null)
      entryOptions.SetAbsoluteExpiration(options.AbsoluteExpirationRelativeToNow.Value);

    if (options?.SlidingExpiration != null)
      entryOptions.SetSlidingExpiration(options.SlidingExpiration.Value);

    memoryCache.Set(key, value, entryOptions);
    return Task.CompletedTask;
  }

  public Task<bool> RemoveAsync(string key)
  {
    memoryCache.Remove(key);
    return Task.FromResult(true);
  }

  public Task<bool> ExistsAsync(string key)
    => Task.FromResult(memoryCache.TryGetValue(key, out _));
}

