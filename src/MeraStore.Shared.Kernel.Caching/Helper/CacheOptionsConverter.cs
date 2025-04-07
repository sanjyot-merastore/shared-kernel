using MeraStore.Shared.Kernel.Caching.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace MeraStore.Shared.Kernel.Caching.Extensions.Helper;

public static class CacheOptionsConverter
{
  public static TimeSpan? ResolveRedisExpiration(CacheEntryOptions options)
  {
    // Redis only supports absolute expiration natively
    return options.AbsoluteExpirationRelativeToNow;
  }

  public static MemoryCacheEntryOptions ToMemoryOptions(CacheEntryOptions options)
  {
    var memoryOptions = new MemoryCacheEntryOptions();

    if (options.AbsoluteExpirationRelativeToNow.HasValue)
      memoryOptions.SetAbsoluteExpiration(options.AbsoluteExpirationRelativeToNow.Value);

    if (options.SlidingExpiration.HasValue)
      memoryOptions.SetSlidingExpiration(options.SlidingExpiration.Value);

    return memoryOptions;
  }
}
