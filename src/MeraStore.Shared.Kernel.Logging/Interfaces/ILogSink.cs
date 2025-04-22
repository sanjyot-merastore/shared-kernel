namespace MeraStore.Shared.Kernel.Logging.Interfaces;

public interface ILogSink
{
  Task WriteAsync(ILog logEntry);
}