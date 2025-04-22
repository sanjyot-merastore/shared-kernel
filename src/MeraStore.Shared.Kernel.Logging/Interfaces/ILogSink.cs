namespace MeraStore.Shared.Kernel.Logging.Interfaces;

public interface ILogSink
{
  Task WriteAsync(ILog logEntry);

  // Optional batch support
  Task WriteBatchAsync(IEnumerable<ILog> logs)
  {
    // Default fallback: process logs individually
    var tasks = logs.Select(WriteAsync);
    return Task.WhenAll(tasks);
  }
}