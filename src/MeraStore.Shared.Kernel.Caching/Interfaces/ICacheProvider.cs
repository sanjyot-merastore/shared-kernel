namespace MeraStore.Shared.Kernel.Caching.Interfaces;

public interface ICacheProvider
{
  Task<T?> GetAsync<T>(string key);
  Task SetAsync<T>(string key, T value, CacheEntryOptions? options = null);
  Task<bool> RemoveAsync(string key);
  Task<bool> ExistsAsync(string key);
}