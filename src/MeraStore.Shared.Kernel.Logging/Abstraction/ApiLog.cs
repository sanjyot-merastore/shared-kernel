using MeraStore.Shared.Kernel.Logging.Attributes;
using Serilog.Events;

namespace MeraStore.Shared.Kernel.Logging.Abstraction;

public class ApiLog : BaseLog
{
  public ApiLog()
  {

  }
  public ApiLog(string message)
  {
    Message = message;
  }

  [LogField("request-url")]
  public string RequestUrl { get; set; } = string.Empty;

  [LogField("request-base-url")]
  public string RequestBaseUrl { get; set; } = string.Empty;

  [LogField("request-path")]
  public string RequestPath { get; set; } = string.Empty;

  [LogField("request-protocol")]
  public string RequestProtocol { get; set; } = string.Empty;

  // Prefixing for Query String and Headers
  [LogField("querystring", true)]
  public Dictionary<string, string> QueryString { get; set; } = new();

  [LogField("rq-header", true)]
  public Dictionary<string, string> RequestHeaders { get; set; } = new();

  [LogField("rs-header", true)]
  public Dictionary<string, string> ResponseHeaders { get; set; } = new();

  [LogField("request")]
  public string Request { get; set; }

  [LogField("response")]
  public string Response { get; set; }

  [LogField("status-code")]
  public int ResponseStatusCode { get; set; }

  [LogField("request-size-bytes")]
  public long RequestSizeBytes { get; set; }

  [LogField("response-size-bytes")]
  public long ResponseSizeBytes { get; set; }


  [LogField("user-id")]
  public string UserId { get; set; }

  [LogField("client-id")]
  public string ClientId { get; set; }

  [LogField("device-id")]
  public string DeviceId { get; set; }

  [LogField("session-id")]
  public string SessionId { get; set; }

  [LogField("tenant-id")]
  public string TenantId { get; set; }

  [LogField("roles")]
  public string Roles { get; set; }

  [LogField("scope")]
  public string Scope { get; set; }

  public new LogEventLevel Level => LogEventLevel.Information;

}