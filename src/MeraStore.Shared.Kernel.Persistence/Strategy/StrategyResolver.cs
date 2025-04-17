using MeraStore.Shared.Kernel.Persistence.Enums;
using MeraStore.Shared.Kernel.Persistence.Interfaces;

namespace MeraStore.Shared.Kernel.Persistence.Strategy;

/// <summary>
/// Resolves the correct <see cref="ICommitStrategy"/> based on the strategy type.
/// </summary>
public class StrategyResolver : IStrategyResolver
{
  private readonly Dictionary<CommitType, ICommitStrategy> _strategies;

  /// <summary>
  /// Initializes a new instance of the <see cref="StrategyResolver"/> class.
  /// </summary>
  /// <param name="strategies">The list of available strategies to resolve from.</param>
  public StrategyResolver(IEnumerable<ICommitStrategy> strategies)
  {
    _strategies = strategies.ToDictionary(s => s.StrategyType);
  }

  /// <summary>
  /// Resolves the appropriate <see cref="ICommitStrategy"/> based on the strategy type.
  /// </summary>
  /// <param name="strategyType">The strategy type to resolve.</param>
  /// <returns>The resolved <see cref="ICommitStrategy"/>.</returns>
  /// <exception cref="NotSupportedException">Thrown if the strategy type is not registered.</exception>
  public ICommitStrategy Resolve(CommitType strategyType)
  {
    if (_strategies.TryGetValue(strategyType, out var strategy))
      return strategy;

    throw new NotSupportedException($"Save strategy {strategyType} not registered.");
  }
}