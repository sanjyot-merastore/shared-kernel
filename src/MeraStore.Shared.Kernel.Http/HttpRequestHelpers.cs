using MeraStore.Shared.Kernel.Logging;
using MeraStore.Shared.Kernel.Logging.Loggers;
using System.Diagnostics;

namespace MeraStore.Shared.Kernel.Http;

/// <summary>
/// Provides extension methods for sending HTTP requests with built-in logging, masking, and resilience.
/// </summary>
public static class HttpRequestHelpers
{
    private static readonly HttpClient DefaultClient = new();

    /// <summary>
    /// Sends an HTTP request with resilience policies, logging, and optional masking.
    /// </summary>
    /// <param name="context">The <see cref="HttpRequest"/> context.</param>
    /// <param name="message">Optional message label for log tracking.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The <see cref="HttpResponseMessage"/> received from the server.</returns>
    public static async Task<HttpResponseMessage> SendAsync(this HttpRequest context, string message = "http_call",
        CancellationToken cancellationToken = default)
    {
        var client = context.Client ?? DefaultClient;

        var apiLog = await BuildApiLogAsync(context, message);

        var stopwatch = Stopwatch.StartNew();
        var response = await HttpResiliencePolicies.DefaultResiliencePolicy
            .ExecuteAsync(ct => client.SendAsync(context.Request, ct), cancellationToken);
        stopwatch.Stop();

        await FinalizeAndLogAsync(apiLog, response, stopwatch.ElapsedMilliseconds);

        if (context.Client == null)
            client.Dispose();

        return response;
    }

    private static async Task<ApiLog> BuildApiLogAsync(HttpRequest context, string message)
    {
        var requestUri = context.Request.RequestUri;

        var apiLog = new ApiLog(message)
        {
            RequestTimestamp = DateTime.UtcNow,
            HttpMethod = context.Method.Method,
            CorrelationId = context.CorrelationId,
            RequestId = context.RequestId,
            RequestBaseUrl = requestUri?.GetLeftPart(UriPartial.Authority) ?? string.Empty,
            RequestPath = requestUri?.AbsolutePath ?? string.Empty,
            RequestProtocol = requestUri?.Scheme ?? string.Empty,
            RequestHeaders = context.Request.Headers.ToDictionary(h => h.Key, h => string.Join(",", h.Value)),
            Request = await ReadPayloadAsync(context.Request.Content)
        };

        foreach (var filter in context.MaskingFilters)
            apiLog.MaskingFilters.Add(filter);

        foreach (var (key, value) in context.LoggingFields)
            apiLog.TrySetLogField(key, value);

        return apiLog;
    }

    private static async Task FinalizeAndLogAsync(ApiLog apiLog, HttpResponseMessage response, long elapsedMs)
    {
        apiLog.TimeTakenMs = elapsedMs;
        apiLog.ResponseStatusCode = (int)response.StatusCode;
        apiLog.ResponseHeaders = response.Headers.ToDictionary(h => h.Key, h => string.Join(",", h.Value));
        apiLog.Response = await ReadPayloadAsync(response.Content);

        await Logger.LogAsync(apiLog);
    }

    private static async Task<Payload?> ReadPayloadAsync(HttpContent? content)
    {
        if (content == null) return null;
        var raw = await content.ReadAsByteArrayAsync();
        return new Payload(raw);
    }
}