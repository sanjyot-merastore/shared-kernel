# MeraStore.Shared.Kernel.Helpers


## 1. RetryHelper

### ✅ Simple Usage (Default Retry)

```csharp
var result = await RetryHelper.RetryAsync(async () =>
{
    // Your flaky operation here
    return await FetchDataFromApiAsync();
});
```

**What it does:**
- Retries up to **3 times**
- Uses **exponential backoff**
- Adds **random jitter**
- No custom conditions or logging

---

### 💪 Advanced Usage (Custom Options)

```csharp
var result = await RetryHelper.RetryAsync(async () =>
{
    // A fragile call like remote service, DB operation, etc.
    return await FetchUserFromServiceAsync();
},
new RetryPolicyOptions
{
    MaxRetryCount = 5,
    BaseDelay = TimeSpan.FromMilliseconds(500),
    MaxDelay = TimeSpan.FromSeconds(3),
    UseJitter = true,
    Strategy = BackoffStrategy.Exponential,
    MaxTotalDuration = TimeSpan.FromSeconds(15),

    ShouldRetryOnException = ex =>
    {
        // Retry only on transient network failures
        return ex is HttpRequestException || ex is TimeoutException;
    },

    OnRetry = (ex, attempt, delay) =>
    {
        Console.WriteLine($"Retry attempt #{attempt} in {delay.TotalMilliseconds}ms due to: {ex.GetType().Name} - {ex.Message}");
    }
});
```
