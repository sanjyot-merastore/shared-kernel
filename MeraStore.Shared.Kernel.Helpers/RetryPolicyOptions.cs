public class RetryPolicyOptions
{
    public int MaxRetryCount { get; set; } = 3;
    public TimeSpan BaseDelay { get; set; } = TimeSpan.FromMilliseconds(200);
    public TimeSpan MaxDelay { get; set; } = TimeSpan.FromSeconds(5);
    public TimeSpan? MaxTotalDuration { get; set; } = null;
    public bool UseJitter { get; set; } = true;
    public BackoffStrategy Strategy { get; set; } = BackoffStrategy.Exponential;

    public Func<Exception, bool>? ShouldRetryOnException { get; set; } = null;
    public Action<Exception, int, TimeSpan>? OnRetry { get; set; } = null;

    public static RetryPolicyOptions Default => new();
}

public enum BackoffStrategy
{
    Exponential,
    Linear,
    Constant
}