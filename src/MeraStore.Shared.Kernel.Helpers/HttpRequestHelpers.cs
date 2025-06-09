namespace MeraStore.Shared.Kernel.Helpers;

public static class HttpRequestHelpers
{
    /// <summary>
    /// Gets a header value from HttpRequest (ASP.NET Core).
    /// </summary>
    public static string? GetHeaderValue(this HttpRequest request, string headerName) =>
        request.Headers.TryGetValue(headerName, out var values) ? values.FirstOrDefault() : null;

    /// <summary>
    /// Gets a header value from HttpRequestMessage (client-side).
    /// </summary>
    public static string? GetHeaderValue(this HttpRequestMessage request, string headerName) =>
        request.Headers.TryGetValues(headerName, out var values) ? values.FirstOrDefault() : null;

    /// <summary>
    /// Extracts client IP address from HttpContext.
    /// </summary>
    public static string? GetClientIp(this HttpContext context)
    {
        var headers = context.Request.Headers;
        return headers.TryGetValue("X-Forwarded-For", out var forwarded)
            ? forwarded.FirstOrDefault()
            : context.Connection.RemoteIpAddress?.ToString();
    }

    /// <summary>
    /// Extracts client IP from HttpRequestMessage.
    /// </summary>
    public static string? GetClientIp(this HttpRequestMessage request)
    {
        var forwarded = request.GetHeaderValue("X-Forwarded-For");
        return !string.IsNullOrWhiteSpace(forwarded)
            ? forwarded.Split(',').FirstOrDefault()?.Trim()
            : null;
    }

    /// <summary>
    /// Gets correlation ID from HttpContext headers.
    /// </summary>
    public static string? GetCorrelationId(this HttpContext context, string headerName = "ms-correlationid") =>
        context.Request.GetHeaderValue(headerName);

    /// <summary>
    /// Gets correlation ID from HttpRequestMessage headers.
    /// </summary>
    public static string? GetCorrelationId(this HttpRequestMessage request, string headerName = "ms-correlationid") =>
        request.GetHeaderValue(headerName);

    /// <summary>
    /// Reads request body from HttpRequestMessage.
    /// </summary>
    public static async Task<string> ReadBodyAsync(this HttpRequestMessage request) =>
        request.Content == null ? string.Empty : await request.Content.ReadAsStringAsync();

    /// <summary>
    /// Reads request body from ASP.NET HttpRequest.
    /// </summary>
    public static async Task<string> ReadBodyAsync(this HttpRequest request)
    {
        request.EnableBuffering();
        request.Body.Position = 0;

        using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
        var body = await reader.ReadToEndAsync();
        request.Body.Position = 0;
        return body;
    }

    /// <summary>
    /// Adds a header to HttpRequestMessage (overwrites if exists).
    /// </summary>
    public static void AddOrUpdateHeader(this HttpRequestMessage request, string headerName, string value)
    {
        if (request.Headers.Contains(headerName))
            request.Headers.Remove(headerName);

        request.Headers.Add(headerName, value);
    }

    /// <summary>
    /// Sets JSON body on HttpRequestMessage.
    /// </summary>
    public static void SetJsonBody<T>(this HttpRequestMessage request, T payload)
    {
        request.Content = new StringContent(JsonHelper.Serialize<T>(payload), Encoding.UTF8, "application/json");
    }

    /// <summary>
    /// Tries to get a query string value from HttpRequest.
    /// </summary>
    public static string? GetQueryParam(this HttpRequest request, string key) =>
        request.Query.TryGetValue(key, out var values) ? values.FirstOrDefault() : null;
}
