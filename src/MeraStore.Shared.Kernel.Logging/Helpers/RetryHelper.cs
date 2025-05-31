namespace MeraStore.Shared.Kernel.Logging.Helpers;

public static class RetryHelper
{
    public static async Task RetryAsync(Func<Task> action, int maxAttempts, TimeSpan delay)
    {
        int attempt = 0;
        while (true)
        {
            try
            {
                await action();
                break;
            }
            catch when (++attempt < maxAttempts)
            {
                await Task.Delay(delay * attempt); // Exponential-ish backoff
            }
            catch (Exception ex)
            {
                await Console.Error.WriteLineAsync($"[RetryHelper] Max retry attempts reached. Error: {ex.Message}");
                throw; // Optional: rethrow or swallow based on caller's needs
            }
        }
    }
}