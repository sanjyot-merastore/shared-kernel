using MeraStore.Shared.Kernel.Caching.Helper;

using Microsoft.Extensions.Caching.Memory;

namespace MeraStore.Shared.Kernel.Caching.Extensions.Helper;

public static class CacheOptionsConverter
{
  public static MemoryCacheEntryOptions ToMemoryOptions(this CacheEntryOptions options)
  {
    var memoryOptions = new MemoryCacheEntryOptions();

    if (options.AbsoluteExpirationRelativeToNow.HasValue)
      memoryOptions.SetAbsoluteExpiration(options.AbsoluteExpirationRelativeToNow.Value);

    if (options.SlidingExpiration.HasValue)
      memoryOptions.SetSlidingExpiration(options.SlidingExpiration.Value);

    return memoryOptions;
  }
}
