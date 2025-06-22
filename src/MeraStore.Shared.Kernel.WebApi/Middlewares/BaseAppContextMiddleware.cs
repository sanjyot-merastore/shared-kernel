using Microsoft.AspNetCore.Http;
using MeraStore.Shared.Kernel.Context;
using MeraStore.Shared.Kernel.Logging;

namespace MeraStore.Shared.Kernel.WebApi.Middleware;

/// <summary>
/// Middleware that sets up AppContext and logging scopes for traceability.
/// Consumers can inherit and override behavior as needed.
/// </summary>
public class BaseAppContextMiddleware(RequestDelegate next, string serviceName)
{
    private readonly RequestDelegate _next = next ?? throw new ArgumentNullException(nameof(next));
    private readonly string _serviceName = serviceName ?? throw new ArgumentNullException(nameof(serviceName));

    public virtual async Task InvokeAsync(HttpContext context)
    {
        // Construct base context from headers
        var baseContext = AppContextBase.FromHttpContext(context, _serviceName);

        var appContext = new AppContextBase(_serviceName)
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

        // Allow derived classes to customize context injection
        EnrichRequestHeaders(context, appContext);

        using (AppContextScope.BeginScope(appContext))
        {
            context.Response.OnStarting(() =>
            {
                EnrichResponseHeaders(context, appContext);
                return Task.CompletedTask;
            });

            await _next(context);
        }
    }

    /// <summary>
    /// Override this method in derived class to customize request header injection.
    /// </summary>
    protected virtual void EnrichRequestHeaders(HttpContext context, AppContextBase appContext)
    {
        context.Request.Headers[Constants.Logging.LogFields.CorrelationId] = appContext.CorrelationId;
        context.Request.Headers[Constants.Logging.LogFields.TransactionId] = appContext.TransactionId;
        context.Request.Headers[Constants.Logging.LogFields.RequestId] = appContext.RequestId;
    }

    /// <summary>
    /// Override this method in derived class to customize response header propagation.
    /// </summary>
    protected virtual void EnrichResponseHeaders(HttpContext context, AppContextBase appContext)
    {
        context.Response.Headers[Constants.Logging.LogFields.CorrelationId] = appContext.CorrelationId;
    }
}
