using System.Text;
using MeraStore.Shared.Kernel.Logging.Loggers;
using Microsoft.AspNetCore.Http;

namespace MeraStore.Shared.Kernel.Logging.Helpers;

public static class HttpContextLoggingExtensions
{
    public static ApiLog ToApiLog(this HttpContext context, byte[] maskedRequestBody, byte[] maskedResponseBody)
    {
        var request = context.Request;
        var response = context.Response;

        return new ApiLog("app_request")
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
            RequestCookies = request.Cookies.ToDictionary(c => c.Key, c => c.Value)
        };
    }
}