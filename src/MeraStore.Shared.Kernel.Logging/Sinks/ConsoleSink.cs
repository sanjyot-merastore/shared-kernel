using MeraStore.Shared.Kernel.Logging.Helpers;
using MeraStore.Shared.Kernel.Logging.Interfaces;
using Serilog.Parsing;
using ILogger = Serilog.ILogger;

namespace MeraStore.Shared.Kernel.Logging.Sinks;

public class ConsoleLogSink : ILogSink
{
  private readonly ILogger _logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

  // Configure Serilog to write to the console

  public async Task WriteAsync(ILog logEntry)
  {
    var logEvent = new LogEvent(
      DateTimeOffset.Now,
      logEntry.Level.ConvertToSerilogLevel(),
      null,
      new MessageTemplate(logEntry.Message, new List<MessageTemplateToken>()),
      logEntry.LoggingFields.Select(kv => new LogEventProperty(kv.Key, new ScalarValue(kv.Value))).ToList());

    _logger.Write(logEvent);
    await Task.CompletedTask;
  }
}