using Microsoft.Extensions.Caching.Memory;

namespace MeraStore.Shared.Kernel.Caching.Interfaces;

public class CacheEntryOptions
{
  /// <summary>
  /// Absolute expiration relative to now (e.g., 30 minutes from now).
  /// </summary>
  public TimeSpan? AbsoluteExpirationRelativeToNow { get; set; }

  /// <summary>
  /// Sliding expiration resets every time the cached item is accessed.
  /// </summary>
  public TimeSpan? SlidingExpiration { get; set; }

  /// <summary>
  /// Absolute expiration at a specific date/time.
  /// </summary>
  public DateTimeOffset? AbsoluteExpiration { get; set; }

  /// <summary>
  /// Optional priority to control eviction under memory pressure.
  /// (Relevant for in-memory caches only)
  /// </summary>
  public CacheItemPriority Priority { get; set; } = CacheItemPriority.Normal;

  /// <summary>
  /// Optional size value (only used if size limit is set on cache).
  /// </summary>
  public long? Size { get; set; }

  /// <summary>
  /// Add custom tags or metadata (can be helpful in diagnostics or eviction policies).
  /// </summary>
  public Dictionary<string, string>? Metadata { get; set; }
}

public enum CacheStrategy
{
  InMemory,
  Redis
}
