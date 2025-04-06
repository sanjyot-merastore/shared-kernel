using MeraStore.Shared.Kernel.Caching.Interfaces;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace MeraStore.Shared.Kernel.Caching.Providers;

public class RedisCacheProvider(IConnectionMultiplexer connection) : ICacheProvider
{
  private readonly IDatabase _db = connection.GetDatabase();

  public async Task<T?> GetAsync<T>(string key)
  {
    var value = await _db.StringGetAsync(key);
    return value.HasValue ? JsonConvert.DeserializeObject<T>(value!) : default;
  }

  public async Task SetAsync<T>(string key, T value, CacheEntryOptions? options = null)
  {
    var serialized = JsonConvert.SerializeObject(value);

    // Determine expiry (Redis supports only absolute expiration)
    var expiry = options?.AbsoluteExpirationRelativeToNow
                 ?? (options?.AbsoluteExpiration.HasValue == true
                   ? options.AbsoluteExpiration.Value - DateTimeOffset.Now
                   : (TimeSpan?)null);

    await _db.StringSetAsync(key, serialized, expiry);
  }

  public async Task<bool> RemoveAsync(string key)
    => await _db.KeyDeleteAsync(key);

  public async Task<bool> ExistsAsync(string key)
    => await _db.KeyExistsAsync(key);
}