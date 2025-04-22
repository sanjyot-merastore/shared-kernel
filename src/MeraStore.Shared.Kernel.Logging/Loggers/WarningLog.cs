using MeraStore.Shared.Kernel.Logging.Attributes;

using Serilog.Events;

namespace MeraStore.Shared.Kernel.Logging.Loggers;

public class WarningLog : BaseLog
{
  public WarningLog(string message, string category = null) : base(message, category)
  {
  }

  public WarningLog(string message) : base(message)
  {
  }

  [LogField("warning-message")]
  public string WarningMessage { get; set; } = string.Empty;
  public new LogEventLevel Level => LogEventLevel.Warning;

  public Dictionary<string, object> AdditionalData { get; set; } = new();
}