using MeraStore.Shared.Kernel.Logging.Interfaces;
using System.Runtime.CompilerServices;
using MeraStore.Shared.Kernel.Logging.Loggers;

namespace MeraStore.Shared.Kernel.Logging;

public static class Logger
{
  // LogSync method (synchronous logging)
  public static void Log(ILog log, [CallerFilePath] string sourceFilePath = "")
  {
    InjectSourceContext(log, sourceFilePath);
    LogWriter.WriteLog(log);
  }

  // LogAsync method (asynchronous logging)
  public static async Task LogAsync(ILog log, [CallerFilePath] string sourceFilePath = "")
  {
    InjectSourceContext(log, sourceFilePath);
    await LogWriter.WriteLogAsync(log);
  }

  // Helper to inject source-context if not already set
  private static void InjectSourceContext(ILog log, string sourceFilePath)
  {
    if (log is BaseLog baseLog)
    {
      var context = Path.GetFileNameWithoutExtension(sourceFilePath); // e.g., WeatherForecastController
      if (!baseLog.LoggingFields.ContainsKey("source-context"))
      {
        baseLog.TrySetLogField("source-context", context);
      }
    }
  }
}