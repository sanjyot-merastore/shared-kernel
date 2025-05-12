using MeraStore.Shared.Kernel.Logging.Attributes;

namespace MeraStore.Shared.Kernel.Logging.Loggers;

public class TraceLog : BaseLog
{
  public TraceLog(string message, string category = null) : base(message, category)
  {
  }

  public TraceLog(string message) : base(message)
  {
  }

  [LogField("db-query-time-ms")]
  public long DbQueryTimeMs { get; set; }

  [LogField("external-api-time-ms")]
  public long ExternalApiTimeMs { get; set; }

  [LogField("cache-lookup-time-ms")]
  public long CacheLookupTimeMs { get; set; }

  public override string GetLevel() => LogLevels.Trace;
}