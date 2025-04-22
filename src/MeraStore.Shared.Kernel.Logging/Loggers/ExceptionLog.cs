using MeraStore.Shared.Kernel.Logging.Attributes;

using Serilog.Events;

namespace MeraStore.Shared.Kernel.Logging.Loggers;

public class ExceptionLog : BaseLog
{
  public ExceptionLog(string message, string category = null) : base(message, category)
  {
  }

  public ExceptionLog(string message) : base(message)
  {
  }

  [LogField("ex-message")]
  public string ExceptionMessage { get; set; } = string.Empty;

  [LogField("ex-stack-trace")]
  public string ExceptionStackTrace { get; set; }

  [LogField("inner-exception")]
  public string InnerExceptionMessage { get; set; }

  [LogField("ex-type")]
  public string ExceptionType { get; set; } = string.Empty;

  [LogField("ex-details")]
  public string ExceptionDetails { get; set; }

  [LogField("is-handled-exception")]
  public bool IsHandledException { get; set; }



  [LogField("status-code")]
  public int? ResponseStatusCode { get; set; }

  public new LogEventLevel Level => LogEventLevel.Error;
}