using System.Text;
using MeraStore.Shared.Kernel.Logging.Loggers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace MeraStore.Shared.Kernel.Logging.Helpers;

public static class HttpContextLoggingExtensions
{
    public static ApiLog ToApiLog(this HttpContext context, byte[] maskedRequestBody, byte[] maskedResponseBody)
    {
        var request = context.Request;
        var response = context.Response;

        var log = new ApiLog("app_request")
        {
            HttpMethod = request.Method,
            RequestUrl = $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}",
            RequestBaseUrl = $"{request.Scheme}://{request.Host}",
            RequestPath = request.Path,
            RequestProtocol = request.Protocol,
            RequestTimestamp = DateTime.UtcNow,

            RequestSizeBytes = maskedRequestBody?.Length ?? 0,
            ResponseSizeBytes = maskedResponseBody?.Length ?? 0,
            ResponseStatusCode = response.StatusCode,

            Request = new Payload(Encoding.UTF8.GetString(maskedRequestBody ?? [])),
            Response = new Payload(Encoding.UTF8.GetString(maskedResponseBody ?? [])),

            QueryString = request.Query.ToDictionary(q => q.Key, q => q.Value.ToString()),
            RequestHeaders = request.Headers.ToDictionary(h => h.Key.ToLowerInvariant(), h => h.Value.ToString()),
            ResponseHeaders = response.Headers.ToDictionary(h => h.Key.ToLowerInvariant(), h => h.Value.ToString()),
            RequestCookies = request.Cookies.ToDictionary(c => c.Key, c => c.Value),
            ResponseCookies = response.Headers.TryGetValue("Set-Cookie", out var setCookies)
                ? new Dictionary<string, string> { { "set-cookie", setCookies.ToString() } }
                : new()
        };

        // ────────────────────────────────
        // Additional Fields via TrySetLogField
        // ────────────────────────────────
        log.TrySetLogField("response-status-message", response.HttpContext.Features.Get<IHttpResponseFeature>()?.ReasonPhrase ?? string.Empty);
        log.TrySetLogField("request-referer", request.Headers["Referer"].ToString());
        log.TrySetLogField("request-content-encoding", request.Headers["Content-Encoding"].ToString());
        log.TrySetLogField("request-accept-language", request.Headers["Accept-Language"].ToString());
        log.TrySetLogField("request-protocol-version", request.Protocol);
        log.TrySetLogField("response-content-encoding", response.Headers["Content-Encoding"].ToString());
        log.TrySetLogField("response-cache-status", response.Headers["Age"].ToString());
        log.TrySetLogField("response-transfer-encoding", response.Headers["Transfer-Encoding"].ToString());

        return log;
    }

}