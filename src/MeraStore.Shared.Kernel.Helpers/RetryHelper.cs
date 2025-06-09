namespace MeraStore.Shared.Kernel.Helpers;

public static class RetryHelper
{
    public static async Task<T> RetryAsync<T>(
        Func<Task<T>> operation,
        RetryPolicyOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        options ??= RetryPolicyOptions.Default;

        var attempt = 0;
        var startTime = DateTime.UtcNow;

        while (true)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                return await operation();
            }
            catch (Exception ex) when (ShouldRetry(ex, attempt, options, startTime))
            {
                attempt++;

                var delay = CalculateDelay(attempt, options);
                options.OnRetry?.Invoke(ex, attempt, delay);

                await Task.Delay(delay, cancellationToken);
            }
        }
    }
    private static bool ShouldRetry(Exception ex, int attempt, RetryPolicyOptions options, DateTime startTime)
    {
        if (attempt >= options.MaxRetryCount)
            return false;

        if (options.MaxTotalDuration.HasValue &&
            DateTime.UtcNow - startTime > options.MaxTotalDuration.Value)
            return false;

        if (options.ShouldRetryOnException != null &&
            !options.ShouldRetryOnException(ex))
            return false;

        return true;
    }

    private static TimeSpan CalculateDelay(int attempt, RetryPolicyOptions options)
    {
        var baseDelay = options.BaseDelay.TotalMilliseconds;
        double delay;

        switch (options.Strategy)
        {
            case BackoffStrategy.Linear:
                delay = baseDelay * attempt;
                break;
            case BackoffStrategy.Constant:
                delay = baseDelay;
                break;
            case BackoffStrategy.Exponential:
            default:
                delay = baseDelay * Math.Pow(2, attempt - 1);
                break;
        }

        if (options.UseJitter)
        {
            var jitter = Random.Shared.Next(0, 100);
            delay += jitter;
        }

        return TimeSpan.FromMilliseconds(Math.Min(delay, options.MaxDelay.TotalMilliseconds));
    }
}