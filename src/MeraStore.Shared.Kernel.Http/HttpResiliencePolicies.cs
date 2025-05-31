using System.Net;
using Polly;

namespace MeraStore.Shared.Kernel.Http;

/// <summary>
/// Provides reusable and configurable Polly-based resilience policies for HTTP requests,
/// including retry, timeout, circuit breaker, bulkhead isolation, and fallback.
/// </summary>
public static class HttpResiliencePolicies
{
    /// <summary>
    /// A default composed policy combining retry, circuit breaker, and timeout policies.
    /// </summary>
    public static readonly IAsyncPolicy<HttpResponseMessage> DefaultResiliencePolicy =
        Policy.WrapAsync(
            RetryPolicy(),
            AdvancedCircuitBreakerPolicy,
            TimeoutPerTryPolicy
        );

    /// <summary>
    /// Creates a retry policy with exponential backoff.
    /// Retries on <see cref="HttpRequestException"/> or non-success status codes.
    /// </summary>
    /// <param name="retryCount">Number of retry attempts.</param>
    /// <param name="initialDelay">Initial delay before first retry (default is 1 second).</param>
    /// <param name="backoffFactor">Multiplier applied to delay for exponential backoff.</param>
    /// <returns>A retry <see cref="IAsyncPolicy{HttpResponseMessage}"/>.</returns>
    public static IAsyncPolicy<HttpResponseMessage> RetryPolicy(
        int retryCount = 3,
        TimeSpan? initialDelay = null,
        double backoffFactor = 2)
    {
        var baseDelay = initialDelay ?? TimeSpan.FromSeconds(1);

        return Policy<HttpResponseMessage>
            .Handle<HttpRequestException>()
            .OrResult(response => !response.IsSuccessStatusCode)
            .WaitAndRetryAsync(
                retryCount,
                retryAttempt => TimeSpan.FromMilliseconds(baseDelay.TotalMilliseconds * Math.Pow(backoffFactor, retryAttempt - 1))
            );
    }

    /// <summary>
    /// Creates a retry policy that specifically handles 500 Internal Server Error
    /// and 429 Too Many Requests by retrying with incremental backoff.
    /// </summary>
    public static IAsyncPolicy<HttpResponseMessage> RetryOnStatusCodePolicy =>
        Policy
            .HandleResult<HttpResponseMessage>(response =>
                response.StatusCode is HttpStatusCode.InternalServerError or HttpStatusCode.TooManyRequests)
            .WaitAndRetryAsync(
                retryCount: 3,
                retryAttempt => TimeSpan.FromSeconds(retryAttempt)
            );

    /// <summary>
    /// Creates a circuit breaker policy that trips when the failure threshold is met.
    /// </summary>
    public static IAsyncPolicy<HttpResponseMessage> AdvancedCircuitBreakerPolicy =>
        Policy<HttpResponseMessage>
            .Handle<HttpRequestException>()
            .OrResult(response => !response.IsSuccessStatusCode)
            .AdvancedCircuitBreakerAsync(
                failureThreshold: 0.5, // 50% failure rate
                samplingDuration: TimeSpan.FromSeconds(30),
                minimumThroughput: 10,
                durationOfBreak: TimeSpan.FromSeconds(30)
            );

    /// <summary>
    /// Creates a timeout policy that limits the duration of a single try to 60 seconds.
    /// </summary>
    public static IAsyncPolicy<HttpResponseMessage> TimeoutPerTryPolicy =>
        Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(60));

    /// <summary>
    /// Creates a bulkhead isolation policy to limit concurrent HTTP requests.
    /// </summary>
    public static IAsyncPolicy<HttpResponseMessage> BulkheadPolicy =>
        Policy.BulkheadAsync<HttpResponseMessage>(
            maxParallelization: 20,
            maxQueuingActions: 10
        );

    /// <summary>
    /// Creates a fallback policy that returns a 503 Service Unavailable response on unhandled failures.
    /// </summary>
    public static IAsyncPolicy<HttpResponseMessage> FallbackPolicy =>
        Policy<HttpResponseMessage>
            .Handle<Exception>()
            .FallbackAsync(
                fallbackAction: cancellationToken =>
                {
                    var fallbackResponse = new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
                    {
                        Content = new StringContent("Service is currently unavailable. Please try again later.")
                    };
                    return Task.FromResult(fallbackResponse);
                }
            );
}
