
namespace MeraStore.Shared.Kernel.Logging.Helpers;

/// <summary>
/// Helper class for converting custom log levels to Serilog's LogEventLevel.
/// </summary>
public static class LogLevelHelper
{
  /// <summary>
  /// Converts a custom log level (as a string) to a Serilog LogEventLevel.
  /// </summary>
  /// <param name="logLevel">The custom log level as a string (e.g., "info", "warn", "error").</param>
  /// <returns>The corresponding Serilog LogEventLevel.</returns>
  internal static LogEventLevel ConvertToSerilogLevel(this string logLevel)
  {
    // Normalize the input to lowercase to make the comparison case-insensitive
    string normalizedLevel = logLevel?.ToLower() ?? string.Empty;

    return normalizedLevel switch
    {
      "trace" => LogEventLevel.Verbose,  // Verbose is for detailed information, usually used for tracing
      "debug" => LogEventLevel.Debug,    // Debug is for debugging information, more detailed than Info
      "info" => LogEventLevel.Information, // Information level for general logging
      "warn" => LogEventLevel.Warning,  // Warning indicates potential issues
      "error" => LogEventLevel.Error,    // Error indicates an error in the flow
      "exception" => LogEventLevel.Error,    // Error indicates an error in the flow
      "fatal" => LogEventLevel.Fatal,    // Fatal indicates a serious error, often leading to application shutdown
      _ => LogEventLevel.Information,    // Default to Information if the level is unknown
    };
  }
}