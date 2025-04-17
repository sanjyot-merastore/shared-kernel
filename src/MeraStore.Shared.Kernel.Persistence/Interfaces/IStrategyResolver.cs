using MeraStore.Shared.Kernel.Persistence.Enums;

namespace MeraStore.Shared.Kernel.Persistence.Interfaces;

/// <summary>
/// Defines a contract for resolving the correct save changes strategy based on the strategy type.
/// </summary>
public interface IStrategyResolver
{
  /// <summary>
  /// Resolves the appropriate <see cref="ICommitStrategy"/> based on the strategy type.
  /// </summary>
  /// <param name="strategyType">The type of save strategy to resolve.</param>
  /// <returns>The resolved save changes strategy.</returns>
  ICommitStrategy Resolve(CommitType strategyType);
}