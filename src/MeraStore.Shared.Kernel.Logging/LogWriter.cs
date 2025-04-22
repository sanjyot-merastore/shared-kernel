using Microsoft.Extensions.Logging;
using MeraStore.Shared.Kernel.Logging.Interfaces;
using MeraStore.Shared.Kernel.Logging.Loggers;

namespace MeraStore.Shared.Kernel.Logging;

public class LogWriter
{
  private static List<ILogSink> _sinks = [];

  public static void Configure(params ILogSink[] sinks)
  {
    _sinks = sinks.ToList();
  }

  public static async Task TraceAsync(TraceLog log) => await WriteAsync(LogLevel.Trace, log);
  public static async Task WarningAsync(WarningLog log) => await WriteAsync(LogLevel.Warning, log);
  public static async Task ApiAsync(ApiLog log) => await WriteAsync(LogLevel.Information, log);
  public static async Task ExceptionAsync(ExceptionLog log) => await WriteAsync(LogLevel.Error, log);

  public static async Task WriteAsync(LogLevel level, ILog log)
  {
    log.TrySetLogField("level", level.ToString());
    log.PopulateLogFields();

    foreach (var sink in _sinks)
    {
      await sink.WriteAsync(log);
    }
  }
}