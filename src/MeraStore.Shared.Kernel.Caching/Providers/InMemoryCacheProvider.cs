using MeraStore.Shared.Kernel.Caching.Extensions.Helper;
using MeraStore.Shared.Kernel.Caching.Interfaces;

using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace MeraStore.Shared.Kernel.Caching.Providers;


public class InMemoryCacheProvider(IMemoryCache memoryCache) : ICacheProvider
{
  public Task<T> GetAsync<T>(string key)
  {
    memoryCache.TryGetValue(key, out var value);
    return Task.FromResult(value is not null ? JsonConvert.DeserializeObject<T>((string)value)! : default!);
  }

  public Task<bool> ExistsAsync(string key)
  {
    return Task.FromResult(memoryCache.TryGetValue(key, out _));
  }

  public Task<bool> RemoveAsync(string key)
  {
    memoryCache.Remove(key);
    return Task.FromResult(true);
  }

  public Task SetAsync<T>(string key, T value, CacheEntryOptions options = null!)
  {
    return SetAsync(key, value, null, options);
  }

  public Task SetAsync<T>(string key, T value, string? idempotencyKey, CacheEntryOptions options = null!)
  {
    if (!string.IsNullOrWhiteSpace(idempotencyKey))
    {
      key = $"{key}:idemp:{idempotencyKey}";
    }

    var serialized = JsonConvert.SerializeObject(value);
    var memoryOptions = options?.ToMemoryOptions() ?? new MemoryCacheEntryOptions();
    memoryCache.Set(key, serialized, memoryOptions);

    return Task.CompletedTask;
  }
}
