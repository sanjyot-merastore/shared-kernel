using System.Diagnostics;
using MeraStore.Shared.Kernel.Context;
using MeraStore.Shared.Kernel.Logging;
using MeraStore.Shared.Kernel.Logging.Helpers;
using MeraStore.Shared.Kernel.Logging.Interfaces;
using MeraStore.Shared.Kernel.Logging.Loggers;
using MeraStore.Shared.Kernel.WebApi.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace MeraStore.Shared.Kernel.WebApi.Middlewares;

/// <summary>
/// Base middleware for logging API requests and responses with masking support.
/// Can be extended to enrich logs or override masking behavior.
/// </summary>
public abstract class BaseLoggingMiddleware(RequestDelegate next, IMaskingFilter maskingFilter, LoggingMiddlewareOptions? options = null)
{
    private readonly RequestDelegate _next = next ?? throw new ArgumentNullException(nameof(next));
    private readonly IMaskingFilter _maskingFilter = maskingFilter ?? throw new ArgumentNullException(nameof(maskingFilter));
    private readonly LoggingMiddlewareOptions? _options = options ?? new LoggingMiddlewareOptions();

    public async Task InvokeAsync(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        if (endpoint?.Metadata?.GetMetadata<SkipApiLoggingAttribute>() is not null)
        {
            await _next(context); // 🚫 Skip logging
            return;
        }

        var path = context.Request.Path.Value ?? string.Empty;
        var method = context.Request.Method;

        if (_options != null &&
            (_options.SkipPaths.Contains(path, StringComparer.OrdinalIgnoreCase) ||
             _options.SkipPathStartsWith.Any(prefix => path.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)) ||
             _options.SkipMethods.Contains(method, StringComparer.OrdinalIgnoreCase)))
        {
            await _next(context); // 🔕 Skip based on config
            return;
        }

        var stopwatch = Stopwatch.StartNew();
        var request = context.Request;
        var response = context.Response;

        context.Request.EnableBuffering();
        var requestBody = await ReadRequestBodyAsync(request);
        context.Request.Body.Position = 0;

        var originalBodyStream = response.Body;
        await using var responseBody = new MemoryStream();
        response.Body = responseBody;

        try
        {
            await _next(context);
        }
        finally
        {
            stopwatch.Stop();

            var maskedRequestBody = await MaskRequestPayloadAsync(request, requestBody);
            var maskedResponseBody = await MaskResponsePayloadAsync(responseBody);

            var apiLog = context.ToApiLog(maskedRequestBody, maskedResponseBody);
            apiLog.CorrelationId = AppContextBase.Current.CorrelationId;
            apiLog.RequestId = AppContextBase.Current.RequestId;
            apiLog.TransactionId = AppContextBase.Current.TransactionId;
            apiLog.TimeTakenMs = stopwatch.ElapsedMilliseconds;

            SetActionAndVerb(apiLog, context);
            AddMaskingFilters(apiLog);

            await EnrichApiLogAsync(apiLog, context);
            await Logger.LogAsync(apiLog);

            response.Body.Seek(0, SeekOrigin.Begin);
            await response.Body.CopyToAsync(originalBodyStream);
        }
    }


    /// <summary>
    /// Sets standardized action and verb names based on the controller/action (MVC) or endpoint metadata (Minimal APIs/FastEndpoints).
    /// Format:
    /// - action = controller_action or endpoint_method (e.g., logging_get_request_payload)
    /// - verb = httpmethod_action (e.g., get_request_payload)
    /// </summary>
    protected virtual void SetActionAndVerb(ApiLog apiLog, HttpContext context)
    {
        var routeData = context.GetRouteData();
        var endpoint = context.GetEndpoint();
        string httpMethod = context.Request?.Method?.ToLowerInvariant() ?? "unknown";

        string? controller = routeData.Values["controller"]?.ToString();
        string? action = routeData.Values["action"]?.ToString();

        // Minimal API / FastEndpoints: use route name if provided
        var routeName = endpoint?.Metadata
            .OfType<Microsoft.AspNetCore.Routing.RouteNameMetadata>()
            .FirstOrDefault()?.RouteName;

        if (!string.IsNullOrWhiteSpace(routeName))
        {
            // Prefer route name if defined
            action = routeName.ToSnakeCase();
        }
        else if (!string.IsNullOrWhiteSpace(controller) && !string.IsNullOrWhiteSpace(action))
        {
            // Use traditional controller/action
            controller = controller.ToSnakeCase();
            action = action.ToSnakeCase();
        }


        string actionName = string.IsNullOrEmpty(controller) ? $"{action}" : $"{controller}_{action}";
        string verbName = $"{httpMethod}_{action}";

        apiLog.TrySetLogField("action", actionName);
        apiLog.TrySetLogField("verb", verbName);
    }

    private static string ExtractMethodName(string displayName)
    {
        if (string.IsNullOrWhiteSpace(displayName))
            return "unknown";

        var method = displayName.Split('(')[0]
            .Split('.')
            .Last()
            .Replace("+", "_");

        return method;
    }


    protected virtual Task<byte[]> MaskRequestPayloadAsync(HttpRequest request, byte[] rawRequestBody)
    {
        return Task.FromResult(_maskingFilter.MaskRequestPayload(rawRequestBody));
    }

    protected virtual Task<byte[]> MaskResponsePayloadAsync(Stream responseStream)
    {
        responseStream.Seek(0, SeekOrigin.Begin);
        using var ms = new MemoryStream();
        responseStream.CopyTo(ms);
        return Task.FromResult(_maskingFilter.MaskResponsePayload(ms.ToArray()));
    }

    protected virtual void AddMaskingFilters(ApiLog apiLog)
    {
        
    }

    protected virtual Task EnrichApiLogAsync(ApiLog apiLog, HttpContext context)
    {
        // Hook to allow adding custom fields (e.g., user ID, roles, custom headers)
        return Task.CompletedTask;
    }

    private async Task<byte[]> ReadRequestBodyAsync(HttpRequest request)
    {
        using var ms = new MemoryStream();
        await request.Body.CopyToAsync(ms);
        return ms.ToArray();
    }

    private static string ToSnakeCase(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return input;

        var stringBuilder = new System.Text.StringBuilder();
        for (int i = 0; i < input.Length; i++)
        {
            var ch = input[i];
            if (char.IsUpper(ch))
            {
                if (i > 0) stringBuilder.Append('_');
                stringBuilder.Append(char.ToLowerInvariant(ch));
            }
            else
            {
                stringBuilder.Append(ch);
            }
        }

        return stringBuilder.ToString();
    }

}
