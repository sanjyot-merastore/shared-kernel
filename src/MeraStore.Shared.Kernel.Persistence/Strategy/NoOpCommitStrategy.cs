using MeraStore.Shared.Kernel.Persistence.Enums;
using MeraStore.Shared.Kernel.Persistence.Interfaces;

namespace MeraStore.Shared.Kernel.Persistence.Strategy;

/// <summary>
/// A no-operation strategy for saving changes, where no actual save occurs.
/// </summary>
public class NoOpCommitStrategy : ICommitStrategy
{
  /// <summary>
  /// Gets the strategy type for the no-op save operation.
  /// </summary>
  public CommitType StrategyType => CommitType.NoOp;

  /// <summary>
  /// Asynchronously does nothing (no-op) for saving changes.
  /// </summary>
  /// <param name="context">The DbContext used for saving changes.</param>
  /// <param name="cancellationToken">The cancellation token to observe during the operation.</param>
  /// <returns>A completed task indicating no changes were made.</returns>
  public Task<int> SaveChangesAsync(DbContext context, CancellationToken cancellationToken = default)
    => Task.FromResult(0);
}