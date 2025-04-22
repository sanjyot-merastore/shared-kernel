using MeraStore.Shared.Kernel.Logging.Attributes;
using MeraStore.Shared.Kernel.Logging.Interfaces;

using Serilog.Events;

namespace MeraStore.Shared.Kernel.Logging.Loggers;

public class ApiLog : BaseLog
{
  public ApiLog(string message, string category = null) : base(message, category)
  {
  }

  public ApiLog(string message) : base(message)
  {
  }


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
  public Payload Request { get; set; }

  [LogField("response")]
  public Payload Response { get; set; }

  [LogField("status-code")]
  public int ResponseStatusCode { get; set; }

  [LogField("request-size-bytes")]
  public long RequestSizeBytes { get; set; }

  [LogField("response-size-bytes")]
  public long ResponseSizeBytes { get; set; }

  [LogField("client-id")]
  public string ClientId { get; set; }


  [LogField("roles")]
  public string Roles { get; set; }

  [LogField("scope")]
  public string Scope { get; set; }

  public new LogEventLevel Level => LogEventLevel.Information;

  public ICollection<ILogFilter> MaskingFilters { get; set; } = [];

  public override Dictionary<string, string> PopulateLogFields()
  {
     var fields = base.PopulateLogFields();


     return fields;
  }
}
