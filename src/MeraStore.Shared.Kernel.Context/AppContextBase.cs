using Microsoft.AspNetCore.Http;

using System.Diagnostics;

namespace MeraStore.Shared.Kernel.Context;

/// <summary>
/// Holds per-request scoped context data such as correlation IDs, tenant info, etc.
/// </summary>
public class AppContextBase
{
  public AppContextBase()
  {
    
  }
  public AppContextBase(string serviceName)
  {
    ServiceName = serviceName ?? throw new ArgumentNullException(nameof(serviceName));
  }

  public string ServiceName { get; protected set; }
  public string CorrelationId { get; set; }
  public string TransactionId { get; set; }
  public string RequestId { get; set; }
  public string TraceId { get; set; }
  public string TenantId { get; set; }
  public string SessionId { get; set; }
  public string UserToken { get; set; } // Optional, useful for internal forwarding/logging
  public DateTime? RequestStartTimestamp { get; set; }

  public Dictionary<string, string> Headers { get; set; } = new(StringComparer.OrdinalIgnoreCase);

  public static AppContextBase Current => AppContextScope.Current;

  public AppContextBase Clone() => new AppContextBase(ServiceName)
  {
    CorrelationId = CorrelationId,
    TransactionId = TransactionId,
    RequestId = RequestId,
    TraceId = TraceId,
    TenantId = TenantId,
    SessionId = SessionId,
    UserToken = UserToken,
    RequestStartTimestamp = RequestStartTimestamp,
    Headers = new Dictionary<string, string>(Headers, StringComparer.OrdinalIgnoreCase)
  };

  public static AppContextBase FromHttpContext(HttpContext httpContext, string serviceName)
  {
    switch (httpContext)
    {
      case null:
        throw new ArgumentNullException(nameof(httpContext));
      default:
      {
        var headers = httpContext.Request.Headers;

        return new AppContextBase(serviceName)
        {
          CorrelationId = headers.TryGetValue("ms-correlationid", out var cid) ? cid.ToString() : Guid.NewGuid().ToString(),
          TransactionId = headers.TryGetValue("ms-txn-id", out var tid) ? tid.ToString() : Guid.NewGuid().ToString(),
          RequestId = Guid.NewGuid().ToString(),
          TraceId = Activity.Current?.TraceId.ToString() ?? Guid.NewGuid().ToString(),
          SessionId = headers.TryGetValue("ms-sessionid", out var sid) ? sid.ToString() : null,
          TenantId = headers.TryGetValue("ms-tenantid", out var tenantId) ? tenantId.ToString() : null,
          UserToken = headers.TryGetValue("Authorization", out var token) ? token.ToString() : null,
          RequestStartTimestamp = DateTime.UtcNow,
          Headers = headers.ToDictionary(h => h.Key, h => h.Value.ToString(), StringComparer.OrdinalIgnoreCase)
        };
      }
    }
  }
}