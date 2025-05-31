using System.Diagnostics;
using MeraStore.Shared.Kernel.Logging;
using MeraStore.Shared.Kernel.Logging.Loggers;
using Polly;

namespace MeraStore.Shared.Kernel.Http;

public static class HttpRequestMessageExtensions
{
    

    private static readonly HttpClient DefaultClient = new();

    public static async Task<HttpResponseMessage> SendAsync(this HttpRequest context, string message = "http_call", CancellationToken cancellationToken = default)
    {
        
        var client = context.Client ?? DefaultClient;

        var apiLog = new ApiLog(message)
        {
            RequestTimestamp = DateTime.UtcNow,
            HttpMethod = context.Method.Method,
            CorrelationId = context.CorrelationId,
            RequestId = context.RequestId,
            RequestBaseUrl = context.Request.RequestUri?.GetLeftPart(UriPartial.Authority) ?? string.Empty,
            RequestPath = context.Request.RequestUri?.AbsolutePath ?? string.Empty,
            RequestProtocol = context.Request.RequestUri?.Scheme ?? string.Empty,
            RequestHeaders = context.Request.Headers.ToDictionary(h => h.Key, h => string.Join(",", h.Value)),
            Request = await ReadPayloadAsync(context.Request.Content),
            TimeTakenMs = 0
        };

        foreach (var f in context.MaskingFilters)
            apiLog.MaskingFilters.Add(f);

        foreach (var (k, v) in context.LoggingFields)
            apiLog.TrySetLogField(k, v);

        var sw = Stopwatch.StartNew();
        var response = await HttpResiliencePolicies.DefaultResiliencePolicy.ExecuteAsync(ct => client.SendAsync(context.Request, ct), cancellationToken);
        sw.Stop();

        apiLog.TimeTakenMs = sw.ElapsedMilliseconds;
        apiLog.ResponseStatusCode = (int)response.StatusCode;
        apiLog.ResponseHeaders = response.Headers.ToDictionary(h => h.Key, h => string.Join(",", h.Value));
        apiLog.Response = await ReadPayloadAsync(response.Content);

        await Logger.LogAsync(apiLog);

        // Dispose if client was locally created
        if (context.Client == null)
        {
            client.Dispose();
        }

        return response;
    }

    private static async Task<Payload?> ReadPayloadAsync(HttpContent? content)
    {
        if (content == null) return null;
        var raw = await content.ReadAsByteArrayAsync();
        return new Payload(raw);
    }
}
