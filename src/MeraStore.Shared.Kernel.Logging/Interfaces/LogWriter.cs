using Microsoft.Extensions.Logging;
using MeraStore.Shared.Kernel.Logging.Loggers;

namespace MeraStore.Shared.Kernel.Logging.Interfaces;

public class LogWriter(IEnumerable<ILogSink> sinks) : ILogWriter
{
  public Task TraceAsync(TraceLog log) => WriteAsync(LogLevel.Trace, log);

  public Task WarningAsync(WarningLog log) => WriteAsync(LogLevel.Warning, log);

  public Task ApiAsync(ApiLog log) => WriteAsync(LogLevel.Information, log);

  public Task ExceptionAsync(ExceptionLog log) => WriteAsync(LogLevel.Error, log);

  public async Task WriteAsync(LogLevel level, ILog log)
  {
    log.TrySetLogField("level", level.ToString());
    log.PopulateLogFields(); // Or let sinks handle this if they're responsible for finalizing fields

    foreach (var sink in sinks)
    {
      await sink.WriteAsync(log);
    }
  }
}