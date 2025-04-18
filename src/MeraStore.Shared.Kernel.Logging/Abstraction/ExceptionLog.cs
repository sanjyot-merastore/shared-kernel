using MeraStore.Shared.Kernel.Logging.Attributes;
using Serilog.Events;

namespace MeraStore.Shared.Kernel.Logging.Abstraction;

public class ExceptionLog : BaseLog
{
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

  // Capturing API request details in case of exception
  [LogField("http-method")]
  public string HttpMethod { get; set; }

  [LogField("request-url")]
  public string RequestUrl { get; set; }


  [LogField("status-code")]
  public int? ResponseStatusCode { get; set; }

  public new LogEventLevel Level => LogEventLevel.Error;
}