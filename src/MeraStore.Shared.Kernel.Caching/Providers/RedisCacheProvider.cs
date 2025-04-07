using MeraStore.Shared.Kernel.Caching.Extensions.Helper;
using MeraStore.Shared.Kernel.Caching.Interfaces;
using Newtonsoft.Json;

namespace MeraStore.Shared.Kernel.Caching.Providers;

public class RedisCacheProvider(IConnectionMultiplexer connection, CacheEntryOptions defaultOptions)
  : ICacheProvider
{
  private readonly IDatabase _db = connection.GetDatabase();

  public async Task<T?> GetAsync<T>(string key)
  {
    var value = await _db.StringGetAsync(key);
    return value.HasValue ? JsonConvert.DeserializeObject<T>(value!) : default;
  }

  public async Task SetAsync<T>(string key, T value, CacheEntryOptions? options = null)
  {
    var effectiveOptions = options ?? defaultOptions;
    var expiration = CacheOptionsConverter.ResolveRedisExpiration(effectiveOptions);

    var serialized = JsonConvert.SerializeObject(value);
    await _db.StringSetAsync(key, serialized, expiration);
  }

  public async Task<bool> RemoveAsync(string key)
    => await _db.KeyDeleteAsync(key);

  public async Task<bool> ExistsAsync(string key)
    => await _db.KeyExistsAsync(key);
}
