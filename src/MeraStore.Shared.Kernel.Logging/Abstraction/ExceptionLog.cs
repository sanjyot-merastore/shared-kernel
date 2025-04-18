using Serilog.Events;

namespace MeraStore.Shared.Kernel.Logging.Abstraction;

public class ExceptionLog : BaseLog
{
  
  public string ExceptionMessage { get; set; } = string.Empty;
  public string ExceptionStackTrace { get; set; }

  public string InnerExceptionMessage { get; set; }

  public string ExceptionType { get; set; } = string.Empty;

  public string ExceptionDetails { get; set; }

  public bool IsHandledException { get; set; }

  public string HttpMethod { get; set; }

  public string RequestUrl { get; set; }


  public int? ResponseStatusCode { get; set; }

  public new LogEventLevel Level => LogEventLevel.Error;
}