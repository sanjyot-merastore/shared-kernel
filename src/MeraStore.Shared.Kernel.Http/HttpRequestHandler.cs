using System.Runtime.CompilerServices;
using MeraStore.Shared.Kernel.Common.Core;
using MeraStore.Shared.Kernel.Logging.Interfaces;
using Polly;

namespace MeraStore.Shared.Kernel.Http;

/// <summary>
/// Provides static helper methods for building, sending, and handling HTTP requests with support for logging, resilience policies, masking filters, and response parsing.
/// </summary>
public static class HttpRequestHandler
{
    /// <summary>
    /// Builds and sends an HTTP request with full logging, resilience policies, masking filters, and response parsing.
    /// </summary>
    /// <typeparam name="TReq">The type of the request payload.</typeparam>
    /// <typeparam name="TRes">The type of the expected response.</typeparam>
    /// <param name="method">The HTTP method to use (GET, POST, PUT, etc.).</param>
    /// <param name="url">The request URL.</param>
    /// <param name="payload">The request payload (optional).</param>
    /// <param name="message">A log message for the request (optional).</param>
    /// <param name="headers">Custom headers to include in the request (optional).</param>
    /// <param name="correlationId">Correlation ID for tracing (optional).</param>
    /// <param name="requestId">Request ID for tracing (optional).</param>
    /// <param name="policies">Resilience policies to apply (optional).</param>
    /// <param name="filters">Masking filters for sensitive data (optional).</param>
    /// <param name="timeout">Request timeout (optional).</param>
    /// <param name="cancellationToken">Cancellation token (optional).</param>
    /// <param name="callerMemberName">The name of the calling method, automatically injected by the compiler.</param>
    /// <returns>An <see cref="ApiResponse{TRes}"/> containing the response or error details.</returns>
    public static async Task<ApiResponse<TRes>> SendAsync<TReq, TRes>(
        HttpMethod method,
        string url,
        TReq? payload = default,
        string message = "http_call",
        Dictionary<string, string>? headers = null,
        string correlationId = "",
        string requestId = "",
        IEnumerable<IAsyncPolicy<HttpResponseMessage>>? policies = null,
        IEnumerable<IMaskingFilter>? filters = null,
        TimeSpan? timeout = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string callerMemberName = "")
    {
        var builder = new HttpRequestBuilder()
            .WithMethod(method)
            .WithUri(url)
            .WithTimeout(timeout ?? TimeSpan.FromSeconds(15));

        if (headers?.Count > 0)
            foreach (var (key, value) in headers)
                builder.WithHeader(key, value);

        // Inject correlation and request IDs as headers, generate new GUIDs if null or empty
        builder.WithCorrelationId(!string.IsNullOrEmpty(correlationId) ? correlationId : Guid.NewGuid().ToString());
        builder.WithRequestId(!string.IsNullOrEmpty(requestId) ? requestId : Guid.NewGuid().ToString());

        if (filters is not null)
            foreach (var filter in filters)
                builder.WithMaskingFilters(filter);

        if (payload is not null)
            builder.WithJsonContent(payload);

        if (policies is not null)
            foreach (var policy in policies)
                builder.AddPolicy(policy);
        else
            builder.UseDefaultResilience();

        var request = builder.Build();
        var response = await request.SendAsync(message, cancellationToken);
        return await response.GetResponseOrFault<TRes>();
    }

    // Convenience helpers for common HTTP verbs

    /// <summary>
    /// Sends an HTTP GET request and parses the response.
    /// </summary>
    /// <typeparam name="TRes">The type of the expected response.</typeparam>
    /// <param name="url">The request URL.</param>
    /// <param name="message">A log message for the request (optional).</param>
    /// <param name="headers">Custom headers to include in the request (optional).</param>
    /// <param name="correlationId">Correlation ID for tracing (optional).</param>
    /// <param name="requestId">Request ID for tracing (optional).</param>
    /// <param name="policies">Resilience policies to apply (optional).</param>
    /// <param name="filters">Masking filters for sensitive data (optional).</param>
    /// <param name="timeout">Request timeout (optional).</param>
    /// <param name="cancellationToken">Cancellation token (optional).</param>
    /// <returns>An <see cref="ApiResponse{TRes}"/> containing the response or error details.</returns>
    public static Task<ApiResponse<TRes>> GetAsync<TRes>(
        string url,
        string message = "http_call",
        Dictionary<string, string>? headers = null,
        string correlationId = "",
        string requestId = "",
        IEnumerable<IAsyncPolicy<HttpResponseMessage>>? policies = null,
        IEnumerable<IMaskingFilter>? filters = null,
        TimeSpan? timeout = null,
        CancellationToken cancellationToken = default, 
        [CallerMemberName] string callerMemberName = "")
        => SendAsync<object, TRes>(HttpMethod.Get, url, null, message, headers, correlationId, requestId, policies, filters, timeout, cancellationToken);

    /// <summary>
    /// Sends an HTTP POST request with a payload and parses the response.
    /// </summary>
    /// <typeparam name="TReq">The type of the request payload.</typeparam>
    /// <typeparam name="TRes">The type of the expected response.</typeparam>
    /// <param name="url">The request URL.</param>
    /// <param name="payload">The request payload.</param>
    /// <param name="message">A log message for the request (optional).</param>
    /// <param name="headers">Custom headers to include in the request (optional).</param>
    /// <param name="correlationId">Correlation ID for tracing (optional).</param>
    /// <param name="requestId">Request ID for tracing (optional).</param>
    /// <param name="policies">Resilience policies to apply (optional).</param>
    /// <param name="filters">Masking filters for sensitive data (optional).</param>
    /// <param name="timeout">Request timeout (optional).</param>
    /// <param name="cancellationToken">Cancellation token (optional).</param>
    /// <param name="callerMemberName">The name of the calling method, automatically injected by the compiler.</param>
    /// <returns>An <see cref="ApiResponse{TRes}"/> containing the response or error details.</returns>
    public static Task<ApiResponse<TRes>> PostAsync<TReq, TRes>(
        string url,
        TReq payload,
        string message = "http_call",
        Dictionary<string, string>? headers = null,
        string correlationId = "",
        string requestId = "",
        IEnumerable<IAsyncPolicy<HttpResponseMessage>>? policies = null,
        IEnumerable<IMaskingFilter>? filters = null,
        TimeSpan? timeout = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string callerMemberName = "")
        => SendAsync<TReq, TRes>(HttpMethod.Post, url, payload, message, headers, correlationId, requestId, policies, filters, timeout, cancellationToken);

    /// <summary>
    /// Sends an HTTP PUT request with a payload and parses the response.
    /// </summary>
    /// <typeparam name="TReq">The type of the request payload.</typeparam>
    /// <typeparam name="TRes">The type of the expected response.</typeparam>
    /// <param name="url">The request URL.</param>
    /// <param name="payload">The request payload.</param>
    /// <param name="message">A log message for the request (optional).</param>
    /// <param name="headers">Custom headers to include in the request (optional).</param>
    /// <param name="correlationId">Correlation ID for tracing (optional).</param>
    /// <param name="requestId">Request ID for tracing (optional).</param>
    /// <param name="policies">Resilience policies to apply (optional).</param>
    /// <param name="filters">Masking filters for sensitive data (optional).</param>
    /// <param name="timeout">Request timeout (optional).</param>
    /// <param name="cancellationToken">Cancellation token (optional).</param>
    /// <param name="callerMemberName">The name of the calling method, automatically injected by the compiler.</param>
    /// <returns>An <see cref="ApiResponse{TRes}"/> containing the response or error details.</returns>
    public static Task<ApiResponse<TRes>> PutAsync<TReq, TRes>(
        string url,
        TReq payload,
        string message = "http_call",
        Dictionary<string, string>? headers = null,
        string correlationId = "",
        string requestId = "",
        IEnumerable<IAsyncPolicy<HttpResponseMessage>>? policies = null,
        IEnumerable<IMaskingFilter>? filters = null,
        TimeSpan? timeout = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string callerMemberName = "")
        => SendAsync<TReq, TRes>(HttpMethod.Put, url, payload, message, headers, correlationId, requestId, policies, filters, timeout, cancellationToken);
}