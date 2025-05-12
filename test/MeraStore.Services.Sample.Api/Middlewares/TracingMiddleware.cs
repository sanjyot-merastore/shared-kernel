using Elastic.Clients.Elasticsearch.Requests;

using MeraStore.Shared.Kernel.Context;
using MeraStore.Shared.Kernel.Logging;

using Serilog.Context;

using System.Diagnostics;

namespace MeraStore.Services.Sample.Api.Middlewares;

/// <summary>
/// Middleware that injects AppContext and logs traceability metadata for each request.
/// </summary>
public class AppContextTracingMiddleware(RequestDelegate next)
{
  private readonly RequestDelegate _next = next ?? throw new ArgumentNullException(nameof(next));

  public async Task InvokeAsync(HttpContext context)
  {
    // Build AppContext from request headers
    var baseContext = AppContextBase.FromHttpContext(context, Constants.ServiceName);

    var appContext = new AppContext(Constants.ServiceName)
    {
      CorrelationId = baseContext.CorrelationId,
      TransactionId = baseContext.TransactionId,
      RequestId = baseContext.RequestId,
      TraceId = baseContext.TraceId,
      TenantId = baseContext.TenantId,
      SessionId = baseContext.SessionId,
      UserToken = baseContext.UserToken,
      RequestStartTimestamp = baseContext.RequestStartTimestamp,
      Headers = baseContext.Headers
    };

    // Add trace headers back into the request if missing (so downstream can see it)
    context.Request.Headers[Constants.Logging.LogFields.CorrelationId] = appContext.CorrelationId;
    context.Request.Headers[Constants.Logging.LogFields.TransactionId] = appContext.TransactionId;
    context.Request.Headers[Constants.Logging.LogFields.RequestId] = appContext.RequestId;
    //context.Request.Headers[Constants.Logging.LogFields.TraceId] = appContext.TraceId;

    using (LogContext.PushProperty(Constants.Logging.LogFields.CorrelationId, appContext.CorrelationId))
    using (LogContext.PushProperty(Constants.Logging.LogFields.TransactionId, appContext.TransactionId))
    using (LogContext.PushProperty(Constants.Logging.LogFields.RequestId, appContext.RequestId))
    using (AppContextScope.BeginScope(appContext))
    {
      // Add trace headers to the response
      context.Response.OnStarting(() =>
      {
        context.Response.Headers[Constants.Logging.LogFields.CorrelationId] = appContext.CorrelationId;
        return Task.CompletedTask;
      });

      await _next(context);
    }
  }
}
