using MeraStore.Shared.Kernel.Context;
using MeraStore.Shared.Kernel.WebApi.Middleware;

namespace MeraStore.Services.Sample.Api.Middlewares;

/// <summary>
/// Middleware that enriches incoming and outgoing HTTP requests
/// with tracing metadata for observability and diagnostics.
/// Inherits from <see cref="BaseAppContextMiddleware"/> to reuse core logic.
/// </summary>
public class TracingMiddleware(RequestDelegate next, string serviceName)
    : BaseAppContextMiddleware(next, serviceName)
{
    /// <summary>
    /// Enriches request headers with trace-related metadata before passing it down the pipeline.
    /// Override to customize header propagation or add new headers.
    /// </summary>
    /// <param name="context">HTTP context for the current request.</param>
    /// <param name="appContext">AppContext instance containing trace data.</param>
    protected override void EnrichRequestHeaders(HttpContext context, AppContextBase appContext)
    {
        base.EnrichRequestHeaders(context, appContext);

    }

    /// <summary>
    /// Enriches response headers with trace identifiers for client-side tracking.
    /// Override to customize header exposure.
    /// </summary>
    /// <param name="context">HTTP context for the current response.</param>
    /// <param name="appContext">AppContext instance containing trace data.</param>
    protected override void EnrichResponseHeaders(HttpContext context, AppContextBase appContext)
    {
        base.EnrichResponseHeaders(context, appContext);
    }
}
