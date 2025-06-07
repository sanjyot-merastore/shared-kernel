using System.Diagnostics;
using System.Runtime.CompilerServices;
using MeraStore.Shared.Kernel.Logging;
using MeraStore.Shared.Kernel.Logging.Loggers;

namespace MeraStore.Shared.Kernel.Http;

/// <summary>
/// Provides extension methods for sending HTTP requests with built-in logging, masking, and resilience.
/// </summary>
public static class HttpRequestHelpers
{
    private static readonly HttpClient DefaultClient = new();

    /// <summary>
    /// Sends an HTTP request using configured resilience policies and logs detailed API telemetry.
    /// Includes support for automatic masking of sensitive fields, caller name tracking,
    /// and structured logging of both request and response data.
    /// </summary>
    /// <param name="context">The <see cref="HttpRequest"/> instance containing request, policies, and logging metadata.</param>
    /// <param name="message">An optional label used to identify the request in logs (default is "http_call").</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <param name="callerMemberName">The name of the calling method, automatically injected by the compiler.</param>
    /// <returns>The <see cref="HttpResponseMessage"/> returned by the target server.</returns>

    public static async Task<HttpResponseMessage> SendAsync(this HttpRequest context, string message = "http_call",
        CancellationToken cancellationToken = default,
        [CallerMemberName] string callerMemberName = "")
    {
        var client = context.Client ?? DefaultClient;

        // Dynamically set the caller name into logging fields
        context.LoggingFields["request-caller"] = callerMemberName;

        var apiLog = await BuildApiLogAsync(context, message);

        var stopwatch = Stopwatch.StartNew();

        var response = await context.FaultPolicy.ExecuteAsync(ct => client.SendAsync(context.Request, ct), cancellationToken);

        stopwatch.Stop();

        await FinalizeAndLogAsync(apiLog, response, stopwatch.ElapsedMilliseconds);

        if (context.Client == null)
            client.Dispose();

        return response;
    }

    private static async Task<ApiLog> BuildApiLogAsync(HttpRequest context, string message)
    {
        var requestUri = context.Request.RequestUri;

        var apiLog = new ApiLog(message);
        apiLog.RequestTimestamp = DateTime.UtcNow;
        apiLog.HttpMethod = context.Method.Method;
        apiLog.CorrelationId = context.CorrelationId;
        apiLog.RequestId = context.RequestId;
        apiLog.RequestBaseUrl = requestUri?.GetLeftPart(UriPartial.Authority) ?? string.Empty;
        apiLog.RequestPath = requestUri?.PathAndQuery ?? string.Empty;
        apiLog.RequestProtocol = requestUri?.Scheme ?? string.Empty;
        apiLog.RequestHeaders = context.Request.Headers.ToDictionary(h => h.Key, h => string.Join(",", h.Value));
        apiLog.Request = await ReadPayloadAsync(context.Request.Content);

        foreach (var filter in context.MaskingFilters)
        {
            apiLog.MaskingFilters.Add(filter);
        }

        foreach (var (key, value) in context.LoggingFields)
        {
            apiLog.TrySetLogField(key, value);
        }

        return apiLog;
    }

    // Dummy placeholder for ReadPayloadAsync
    private static async Task<Payload> ReadPayloadAsync(HttpContent? content)
    {
        if (content == null)
            return new Payload(string.Empty);

        var data = await content.ReadAsStringAsync();
        return new Payload(data);
    }

    // Dummy placeholder for FinalizeAndLogAsync
    private static async Task FinalizeAndLogAsync(ApiLog apiLog, HttpResponseMessage response, long elapsedMilliseconds)
    {
        // Add response data & elapsed time to apiLog, then log
        apiLog.ResponseStatusCode = (int)response.StatusCode;
        apiLog.ResponseSizeBytes = response.Content.Headers.ContentLength ?? 0;

        // Since ApiLog has only headers as dictionary, better pass HttpRequestMessage separately or add RequestMessage property to ApiLog
        // Assuming you add HttpRequestMessage originalRequest to ApiLog or pass it in parameters

        if (apiLog.RequestHeaders != null)
        {
            // Example: Request headers are dictionary<string,string>
            apiLog.TrySetLogField("request-referer", apiLog.RequestHeaders.TryGetValue("Referer", out var referer) ? referer : string.Empty);
            apiLog.TrySetLogField("request-content-encoding", apiLog.RequestHeaders.TryGetValue("Content-Encoding", out var contentEncoding) ? contentEncoding : string.Empty);
            apiLog.TrySetLogField("request-accept-language", apiLog.RequestHeaders.TryGetValue("Accept-Language", out var acceptLanguage) ? acceptLanguage : string.Empty);
        }

        // Request protocol version is tricky without HttpRequestMessage; if you can pass it, do it:

        // Response headers
        var responseContentEncoding = response.Content?.Headers.ContentEncoding;
        apiLog.TrySetLogField("response-content-encoding", responseContentEncoding is { Count: > 0 } ? string.Join(",", responseContentEncoding) : string.Empty);

        apiLog.TrySetLogField("response-cache-status", response.Headers.TryGetValues("Age", out var ages) ? string.Join(",", ages) : string.Empty);

        apiLog.TrySetLogField("response-transfer-encoding", response?.Headers?.TransferEncoding is { Count: > 0 }
            ? string.Join(",", response.Headers.TransferEncoding.Select(te => te.ToString()))
            : string.Empty);

        // Actual logging logic here...
        await Task.CompletedTask;
    }
}