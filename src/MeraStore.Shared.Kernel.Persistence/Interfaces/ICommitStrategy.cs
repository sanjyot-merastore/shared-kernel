using MeraStore.Shared.Kernel.Persistence.Enums;

using Microsoft.EntityFrameworkCore;

namespace MeraStore.Shared.Kernel.Persistence.Interfaces;

/// <summary>
/// Defines a contract for saving changes asynchronously to the database.
/// </summary>
public interface ICommitStrategy
{
  /// <summary>
  /// Gets the strategy type for the save operation.
  /// </summary>
  CommitType StrategyType { get; }

  /// <summary>
  /// Asynchronously saves changes to the database.
  /// </summary>
  /// <param name="context">The DbContext used for saving changes.</param>
  /// <param name="cancellationToken">The cancellation token to observe during the operation.</param>
  /// <returns>The number of state entries written to the database.</returns>
  Task<int> SaveChangesAsync(DbContext context, CancellationToken cancellationToken = default);
}