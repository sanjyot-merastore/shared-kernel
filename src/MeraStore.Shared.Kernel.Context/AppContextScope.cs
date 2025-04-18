namespace MeraStore.Shared.Kernel.Context;

/// <summary>
/// Manages the lifecycle of the AppContextBase using AsyncLocal and a stack-based scope.
/// </summary>
public static class AppContextScope
{
  private static readonly AsyncLocal<Stack<AppContextBase>> _stack = new();

  private static Stack<AppContextBase> ContextStack => _stack.Value ??= new();

  /// <summary>
  /// Gets the current context or a default one.
  /// </summary>
  public static AppContextBase Current => ContextStack.Count > 0
    ? ContextStack.Peek()
    : new AppContextBase();

  /// <summary>
  /// Push a new AppContextBase instance into the current AsyncLocal scope.
  /// </summary>
  public static IDisposable BeginScope(AppContextBase contextBase)
  {
    ContextStack.Push(contextBase ?? throw new ArgumentNullException(nameof(contextBase)));
    return new DisposableScope(ContextStack);
  }

  private sealed class DisposableScope(Stack<AppContextBase> contextStack) : IDisposable
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