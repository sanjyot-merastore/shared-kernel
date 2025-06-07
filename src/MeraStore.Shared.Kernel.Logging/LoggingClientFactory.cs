using MeraStore.Services.Logging.SDK;
using MeraStore.Services.Logging.SDK.Interfaces;
using MeraStore.Shared.Kernel.Exceptions;

namespace MeraStore.Shared.Kernel.Logging;

public static class LoggingClientFactory
{
    private static ILoggingApiClient? _instance;
    private static readonly Lock Lock = new();

    public static void Configure(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw LoggingServiceException.LogConfigurationMissing("Logging URL cannot be null or empty.");

        lock (Lock)
        {
            if (_instance != null)
                return; // Already initialized, ignore

            _instance = new ClientBuilder()
                .WithUrl(url)
                .UseDefaultResiliencePolicy()
                .Build();
        }
    }

    public static ILoggingApiClient Initialize()
    {
        if (_instance == null)
            throw LoggingServiceException.LogInternalServerError("LoggingClientFactory is not configured. Call Configure(url) first.");

        return _instance;
    }
}