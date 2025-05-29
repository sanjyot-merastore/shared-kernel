using System.Net;
using Polly;

namespace MeraStore.Shared.Kernel.Http;

public static class HttpResiliencePolicies
{
  /// <summary>
  /// Retry policy with dynamic retry count, initial delay, and exponential backoff factor.
  /// </summary>
  /// <param name="retryCount">Number of retries.</param>
  /// <param name="initialDelay">Initial delay before first retry.</param>
  /// <param name="backoffFactor">Exponential backoff multiplier.</param>
  /// <returns>IAsyncPolicy for HttpResponseMessage.</returns>
  public static IAsyncPolicy<HttpResponseMessage> RetryPolicy(int retryCount = 3, TimeSpan? initialDelay = null, double backoffFactor = 2)
  {
    var baseDelay = initialDelay ?? TimeSpan.FromSeconds(1);

    return Policy<HttpResponseMessage>
      .Handle<HttpRequestException>()
      .OrResult(r => !r.IsSuccessStatusCode)
      .WaitAndRetryAsync(
        retryCount,
        retryAttempt => TimeSpan.FromMilliseconds(baseDelay.TotalMilliseconds * Math.Pow(backoffFactor, retryAttempt - 1))
      );
  }

  public static IAsyncPolicy<HttpResponseMessage> AdvancedCircuitBreakerPolicy => Policy<HttpResponseMessage>
    .Handle<HttpRequestException>()
    .OrResult(r => !r.IsSuccessStatusCode)
    .AdvancedCircuitBreakerAsync(
      failureThreshold: 0.5, // 50% failure rate
      samplingDuration: TimeSpan.FromSeconds(30),
      minimumThroughput: 10,
      durationOfBreak: TimeSpan.FromSeconds(30));

  public static IAsyncPolicy<HttpResponseMessage> TimeoutPerTryPolicy => Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(60));

  public static IAsyncPolicy<HttpResponseMessage> BulkheadPolicy => Policy
    .BulkheadAsync<HttpResponseMessage>(maxParallelization: 20, maxQueuingActions: 10);

  public static IAsyncPolicy<HttpResponseMessage> FallbackPolicy => Policy<HttpResponseMessage>
    .Handle<Exception>()
    .FallbackAsync(
      fallbackAction: (cancellationToken) =>
      {
        var fallbackResponse = new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
        {
          Content = new StringContent("Service is currently unavailable. Please try again later.")
        };
        return Task.FromResult(fallbackResponse);
      }
    );

  public static IAsyncPolicy<HttpResponseMessage> RetryOnStatusCodePolicy => Policy
    .HandleResult<HttpResponseMessage>(r =>
      r.StatusCode is HttpStatusCode.InternalServerError or HttpStatusCode.TooManyRequests)
    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(retryAttempt));


}