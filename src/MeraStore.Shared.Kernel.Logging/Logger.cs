using MeraStore.Shared.Kernel.Logging.Interfaces;

namespace MeraStore.Shared.Kernel.Logging
{
  public static class Logger
  {
    // LogSync method (synchronous logging)
    public static void Log(ILog log)
    {
      LogWriter.WriteLog(log); // Delegate to LogWriter for synchronous logging
    }

    // LogAsync method (asynchronous logging)
    public static async Task LogAsync(ILog log)
    {
      await LogWriter.WriteLogAsync(log); // Delegate to LogWriter for async logging
    }
  }
}