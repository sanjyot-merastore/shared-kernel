using Elastic.Clients.Elasticsearch.Nodes;
using MeraStore.Services.Logging.SDK;
using MeraStore.Services.Logging.SDK.Models;
using MeraStore.Shared.Kernel.Logging.Attributes;
using MeraStore.Shared.Kernel.Logging.Filters;
using MeraStore.Shared.Kernel.Logging.Interfaces;

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

  [LogField("request-timestamp")]
  public DateTime? RequestTimestamp { get; set; }

  [LogField("client-id")]
  public string ClientId { get; set; }


  [LogField("roles")]
  public string Roles { get; set; }

  [LogField("scope")]
  public string Scope { get; set; }
  public override string GetLevel() => LogLevels.Api;

  public ICollection<IMaskingFilter> MaskingFilters { get; set; } = [new MaskingFilter([], [])];

  public override async Task<Dictionary<string, string>> PopulateLogFields()
  {
    var fields = await base.PopulateLogFields();
    var url = "http://logging-api.merastore.com:8101";
    var loggingClient = new ClientBuilder()
      .WithUrl(url) // Replace with actual service URL
      .UseDefaultResiliencePolicy()
      .Build();


    // Apply masking filters to sensitive fields (e.g., request or response payloads)
    if (MaskingFilters.Any())
    {
      try
      {
        LoggingFields.Remove("request");
        // Apply masking filters to specific fields, e.g., Request and Response
        if (Request != null)
        {
          foreach (var filter in MaskingFilters)
          {
            Request = new Payload(filter.MaskRequestPayload(Request.Data));
          }

          if (Request.Data.Length != 0)
          {
            var requestLog = loggingClient.CreateRequestLogAsync(new RequestLog()
            {
              Payload = Request.Data,
              CorrelationId = CorrelationId,
              HttpMethod = HttpMethod,
              Timestamp = RequestTimestamp ?? DateTime.UtcNow,
              Url = RequestUrl

            }, GetDefaultHeaders(CorrelationId)).Result;
            var requestId = requestLog?.Response?.Id;
            TrySetLogField("request", url + $"/api/v1.0/logs/requests/{requestId}");
          }

        }

        if (Response != null)
        {
          LoggingFields.Remove("response");
          foreach (var filter in MaskingFilters)
          {
            Response = new Payload(filter.MaskResponsePayload(Response.Data));
          }

          var response = loggingClient.CreateResponseLogAsync(new ResponseLog()
          {
            Payload = Response.Data,
            RequestId = Guid.Parse(RequestId)
          }, GetDefaultHeaders(CorrelationId)).Result;
          var requestId = response?.Response?.Id;
          TrySetLogField("response", url + $"/api/v1.0/logs/responses/{requestId}");
        }
      }
      catch (Exception ex)
      {
        // Handle any exceptions that occur during masking
        // You can log the exception or take appropriate action
        Console.WriteLine($"Error applying masking filters: {ex.Message}");
      }

    }
    return fields;
  }

  static IList<KeyValuePair<string, string>> GetDefaultHeaders(string correlationId)
  {
    return new List<KeyValuePair<string, string>>()
    {
      new KeyValuePair<string, string>("ms-correlationId", correlationId )
    };
  }
}
