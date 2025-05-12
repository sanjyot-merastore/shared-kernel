using MeraStore.Shared.Kernel.Logging.Interfaces;

using System.Collections.Concurrent;

namespace MeraStore.Shared.Kernel.Logging;

internal static class LogWriter
{
  private static List<ILogSink> _sinks = [];
  private static readonly ConcurrentQueue<ILog> Queue = new();
  private static Timer? _batchTimer;
  private static readonly SemaphoreSlim Semaphore = new(1, 1); // Protects batch processing
  private static int _batchSize = 20;
  private static TimeSpan _interval = TimeSpan.FromSeconds(2);
  private static bool _isInitialized = false;

  // Configure sinks and start the background processor
  public static void Configure(params ILogSink[] sinks)
  {
    _sinks = sinks.ToList();
    StartBackgroundProcessor();
  }

  // Configure batching parameters
  public static void ConfigureBatching(int batchSize, TimeSpan interval)
  {
    _batchSize = batchSize;
    _interval = interval;
    StartBackgroundProcessor();
  }

  // Add a log to the queue
  public static void WriteLog(ILog log)
  {
    log.TrySetLogField("level", log.GetLevel());
    log.PopulateLogFields();
    Queue.Enqueue(log);
  }

  // Async method to immediately write the log to sinks
  public static async Task WriteLogAsync(ILog log)
  {
    log.TrySetLogField("level", log.GetLevel());
    log.PopulateLogFields();

    foreach (var sink in _sinks)
    {
      await sink.WriteAsync(log);
    }
  }

  // Start the background timer that processes logs in batches
  private static void StartBackgroundProcessor()
  {
    if (_isInitialized) return;

    _isInitialized = true;
    _batchTimer = new Timer(ProcessBatch, null, _interval, _interval);
  }

  // Process a batch of logs
  private static async void ProcessBatch(object? state)
  {
    // Prevent concurrent processing
    if (!await Semaphore.WaitAsync(0)) return;

    try
    {
      var logs = new List<ILog>();

      // Dequeue logs for the batch
      while (logs.Count < _batchSize && Queue.TryDequeue(out var log))
      {
        logs.Add(log);
      }

      if (logs.Count == 0) return; // If there's nothing to process, return

      // Write the batch to all sinks
      foreach (var sink in _sinks)
      {
        try
        {
          await sink.WriteBatchAsync(logs);
        }
        catch
        {
          // Optionally handle sink failure (e.g., retry queue, fallback)
        }
      }
    }
    finally
    {
      // Release semaphore so that the batch processing can be executed again
      Semaphore.Release();
    }
  }
}