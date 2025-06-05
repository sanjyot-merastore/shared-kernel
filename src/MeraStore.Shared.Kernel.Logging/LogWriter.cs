using MeraStore.Shared.Kernel.Logging.Interfaces;

using System.Collections.Concurrent;
using MeraStore.Shared.Kernel.Logging.Helpers;

namespace MeraStore.Shared.Kernel.Logging;

internal static class LogWriter
{
    private static List<ILogSink> _sinks = [];
    private static readonly ConcurrentQueue<ILog> Queue = new();
    private static Timer? _batchTimer;
    private static readonly SemaphoreSlim Semaphore = new(1, 1);
    private static int _batchSize = 20;
    private static TimeSpan _interval = TimeSpan.FromSeconds(2);
    private static bool _isInitialized = false;

    public static void Configure(params ILogSink[] sinks)
    {
        _sinks = sinks.ToList();
        StartBackgroundProcessor();
    }

    public static void ConfigureBatching(int batchSize, TimeSpan interval)
    {
        _batchSize = batchSize;
        _interval = interval;
        StartBackgroundProcessor();
    }

    public static void WriteLog(ILog log)
    {
        try
        {
            log.TrySetLogField("level", log.GetLevel());
            log.PopulateLogFields();
            Queue.Enqueue(log);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[LogWriter] Failed to enqueue log: {ex.Message}");
        }
    }

    public static async Task WriteLogAsync(ILog log)
    {
        try
        {
            log.TrySetLogField("level", log.GetLevel());
            log.PopulateLogFields();

            foreach (var sink in _sinks)
            {
                await RetryHelper.RetryAsync(() => sink.WriteAsync(log), 3, TimeSpan.FromMilliseconds(300));
            }
        }
        catch (Exception ex)
        {
            await Console.Error.WriteLineAsync($"[LogWriter] Failed to write log asynchronously: {ex.Message}");
        }
    }

    private static void StartBackgroundProcessor()
    {
        if (_isInitialized) return;

        _isInitialized = true;
        _batchTimer = new Timer(ProcessBatch, null, _interval, _interval);
    }

    private static async void ProcessBatch(object? state)
    {
        if (!await Semaphore.WaitAsync(0)) return;

        try
        {
            var logs = new List<ILog>();

            while (logs.Count < _batchSize && Queue.TryDequeue(out var log))
            {
                logs.Add(log);
            }

            if (logs.Count == 0) return;

            foreach (var sink in _sinks)
            {
                try
                {
                    await RetryHelper.RetryAsync(() => sink.WriteBatchAsync(logs), 3, TimeSpan.FromMilliseconds(500));
                }
                catch (Exception ex)
                {
                    await Console.Error.WriteLineAsync($"[LogWriter] Sink {sink.GetType().Name} failed to write batch: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            await Console.Error.WriteLineAsync($"[LogWriter] Unexpected error in batch processing: {ex.Message}");
        }
        finally
        {
            Semaphore.Release();
        }
    }
}
