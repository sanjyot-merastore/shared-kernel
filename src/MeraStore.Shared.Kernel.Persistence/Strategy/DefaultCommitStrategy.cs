using MeraStore.Shared.Kernel.Persistence.Enums;
using MeraStore.Shared.Kernel.Persistence.Interfaces;

namespace MeraStore.Shared.Kernel.Persistence.Strategy;

/// <summary>
/// A default strategy for saving changes using the <see cref="DbContext"/>.
/// </summary>
public class DefaultCommitStrategy : ICommitStrategy
{
  /// <summary>
  /// Gets the strategy type for the default save operation.
  /// </summary>
  public CommitType StrategyType => CommitType.Default;

  /// <summary>
  /// Asynchronously saves changes to the database using the <see cref="DbContext"/>.
  /// </summary>
  /// <param name="context">The DbContext used for saving changes.</param>
  /// <param name="cancellationToken">The cancellation token to observe during the operation.</param>
  /// <returns>The number of state entries written to the database.</returns>
  public Task<int> SaveChangesAsync(DbContext context, CancellationToken cancellationToken = default)
    => context.SaveChangesAsync(cancellationToken);
}