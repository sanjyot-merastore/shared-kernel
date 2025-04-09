using MeraStore.Shared.Kernel.Caching.Interfaces;
using Newtonsoft.Json;

namespace MeraStore.Shared.Kernel.Caching.Providers;


public class RedisCacheProvider(IConnectionMultiplexer connection) : ICacheProvider
{
  private readonly IDatabase _db = connection.GetDatabase();

  public async Task<T> GetAsync<T>(string key)
  {
    var value = await _db.StringGetAsync(key);
    return value.HasValue ? JsonConvert.DeserializeObject<T>(value!)! : default!;
  }

  public Task<bool> ExistsAsync(string key)
  {
    return _db.KeyExistsAsync(key);
  }

  public Task<bool> RemoveAsync(string key)
  {
    return _db.KeyDeleteAsync(key);
  }

  public Task SetAsync<T>(string key, T value, CacheEntryOptions options = null!)
  {
    return SetAsync(key, value, null, options);
  }

  public async Task SetAsync<T>(string key, T value, string? idempotencyKey, CacheEntryOptions options = null!)
  {
    if (!string.IsNullOrWhiteSpace(idempotencyKey))
    {
      key = $"{key}:idemp:{idempotencyKey}";
    }

    var serialized = JsonConvert.SerializeObject(value);
    var expiry = options?.AbsoluteExpirationRelativeToNow;
    await _db.StringSetAsync(key, serialized, expiry);
  }
}
