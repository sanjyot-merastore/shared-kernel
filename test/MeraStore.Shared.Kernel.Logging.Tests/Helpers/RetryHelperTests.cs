using MeraStore.Shared.Kernel.Logging.Helpers;

namespace MeraStore.Shared.Kernel.Logging.Tests.Helpers;

public class RetryHelperTests
{
    [Fact]
    public async Task RetryAsync_SucceedsOnFirstAttempt_NoRetries()
    {
        int callCount = 0;

        await RetryHelper.RetryAsync(async () =>
        {
            callCount++;
            await Task.CompletedTask; // Simulate success
        }, maxAttempts: 3, delay: TimeSpan.FromMilliseconds(10));

        Assert.Equal(1, callCount);
    }

    [Fact]
    public async Task RetryAsync_RetriesUntilSuccess()
    {
        int callCount = 0;
        int failTimes = 2; // Fail twice before success

        await RetryHelper.RetryAsync(async () =>
        {
            callCount++;
            if (callCount <= failTimes)
            {
                throw new InvalidOperationException("Failed attempt");
            }
            await Task.CompletedTask;
        }, maxAttempts: 5, delay: TimeSpan.FromMilliseconds(10));

        Assert.Equal(3, callCount);
    }

    [Fact]
    public async Task RetryAsync_ThrowsAfterMaxAttempts()
    {
        int callCount = 0;
        var exceptionMessage = "Persistent failure";

        var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await RetryHelper.RetryAsync(async () =>
            {
                callCount++;
                throw new InvalidOperationException(exceptionMessage);
            }, maxAttempts: 3, delay: TimeSpan.FromMilliseconds(10));
        });

        Assert.Equal(3, callCount);
        Assert.Equal(exceptionMessage, ex.Message);
    }
}