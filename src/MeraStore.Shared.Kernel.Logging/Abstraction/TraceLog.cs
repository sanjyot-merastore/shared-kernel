using MeraStore.Shared.Kernel.Logging.Attributes;
using Serilog.Events;

namespace MeraStore.Shared.Kernel.Logging.Abstraction;

public class TraceLog : BaseLog
{
  
  [LogField("db-query-time-ms")]
  public long DbQueryTimeMs { get; set; }

  [LogField("external-api-time-ms")]
  public long ExternalApiTimeMs { get; set; }

  [LogField("cache-lookup-time-ms")]
  public long CacheLookupTimeMs { get; set; }

  public new LogEventLevel Level => LogEventLevel.Verbose;
}