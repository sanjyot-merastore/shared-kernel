using Serilog.Events;

namespace MeraStore.Shared.Kernel.Logging.Abstraction;

public class TraceLog : BaseLog
{

  public long DbQueryTimeMs { get; set; }

  public long ExternalApiTimeMs { get; set; }

  public long CacheLookupTimeMs { get; set; }

  public new LogEventLevel Level => LogEventLevel.Verbose;
}