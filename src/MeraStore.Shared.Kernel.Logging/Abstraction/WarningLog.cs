using MeraStore.Shared.Kernel.Logging.Attributes;
using Serilog.Events;

namespace MeraStore.Shared.Kernel.Logging.Abstraction;

public class WarningLog : BaseLog
{
  [LogField("warning-message")]
  public string WarningMessage { get; set; } = string.Empty;
  public new LogEventLevel Level => LogEventLevel.Warning;

  public Dictionary<string, object> AdditionalData { get; set; } = new();
}