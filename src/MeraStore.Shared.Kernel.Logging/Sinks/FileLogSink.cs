using MeraStore.Shared.Kernel.Logging.Interfaces;
using Serilog.Parsing;
using MeraStore.Shared.Kernel.Logging.Helpers;

namespace MeraStore.Shared.Kernel.Logging.Sinks;

/// <inheritdoc />
public class FileLogSink(string path) : ILogSink
{
  private readonly ILogger _logger = new LoggerConfiguration()
    .WriteTo.File(path ?? "logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

  // Create a Serilog logger with a rolling file sink

  /// <inheritdoc />
  public async Task WriteAsync(ILog logEntry)
  {
    // Convert logEntry to a LogEvent for Serilog
    var logEvent = new LogEvent(
      DateTimeOffset.Now,              // Timestamp
      logEntry.GetLevel().ConvertToSerilogLevel(),             // Log level (Information, Error, etc.)
      null,                            // Exception (if any)
      new MessageTemplate(logEntry.Message, new List<MessageTemplateToken>()), // Message Template
      logEntry.LoggingFields.Select(kv => new LogEventProperty(kv.Key, new ScalarValue(kv.Value))).ToList() // Log fields
    );

    // Write the log event to the Serilog logger
    _logger.Write(logEvent);

    // Since Serilog's Write method is async, we use Task.CompletedTask as a simple way of signaling the async method
    await Task.CompletedTask;
  }
}