namespace MeraStore.Shared.Kernel.Common.Context;

/// <summary>
/// Manages the lifecycle of the AppContext using AsyncLocal and a stack-based scope.
/// </summary>
public static class AppContextScope
{
  private static readonly AsyncLocal<Stack<AppContext>> _stack = new();

  private static Stack<AppContext> ContextStack => _stack.Value ??= new();

  /// <summary>
  /// Gets the current context or a default one.
  /// </summary>
  public static AppContext Current => ContextStack.Count > 0
    ? ContextStack.Peek()
    : new AppContext();

  /// <summary>
  /// Push a new AppContext instance into the current AsyncLocal scope.
  /// </summary>
  public static IDisposable BeginScope(AppContext context)
  {
    ContextStack.Push(context ?? throw new ArgumentNullException(nameof(context)));
    return new DisposableScope(ContextStack);
  }

  private sealed class DisposableScope(Stack<AppContext> contextStack) : IDisposable
  {
    private bool _disposed;

    public void Dispose()
    {
      if (_disposed) return;

      if (contextStack.Count > 0)
        contextStack.Pop();

      _disposed = true;
    }
  }
}