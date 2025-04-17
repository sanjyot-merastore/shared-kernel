using MeraStore.Shared.Kernel.Caching.Helper;

namespace MeraStore.Shared.Kernel.Caching.Interfaces;

public interface ICacheProvider
{
  CacheStrategy Strategy { get; }
  Task<T> GetAsync<T>(string key);
  Task<bool> ExistsAsync(string key);
  Task<bool> RemoveAsync(string key);

  // Overloads for SetAsync
  Task SetAsync<T>(string key, T value, CacheEntryOptions options = null);
  Task SetAsync<T>(string key, T value, string? idempotencyKey, CacheEntryOptions options = null);
}
