using Serilog.Events;

namespace MeraStore.Shared.Kernel.Logging.Abstraction;

public class WarningLog : BaseLog
{
  public new LogEventLevel Level => LogEventLevel.Warning;
  public string WarningMessage { get; set; }
  public string WarningDetails { get; set; } // E.g., potential issues or configurations
}