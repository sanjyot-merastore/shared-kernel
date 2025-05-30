using MeraStore.Shared.Kernel.Persistence.Enums;
using MeraStore.Shared.Kernel.Persistence.Interfaces;

namespace MeraStore.Shared.Kernel.Persistence.Repositories;

/// <summary>
/// Represents the unit of work that coordinates the changes with the DbContext.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
  private readonly ICommitStrategy _strategy;

  /// <summary>
  /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
  /// </summary>
  /// <param name="context">The DbContext used to save changes.</param>
  /// <param name="resolver">The strategy resolver to resolve the save changes strategy.</param>
  /// <param name="strategyType">The type of save strategy to use.</param>
  public UnitOfWork(DbContext context, IStrategyResolver resolver, CommitType strategyType)
  {
    _strategy = resolver.Resolve(strategyType);
  }

  /// <summary>
  /// Commits the changes to the database using the selected save strategy.
  /// </summary>
  /// <param name="context"></param>
  /// <param name="cancellationToken">The cancellation token to observe during the operation.</param>
  /// <returns>The number of state entries written to the database.</returns>
  public Task<int> CommitAsync(DbContext context, CancellationToken cancellationToken = default)
    => _strategy.SaveChangesAsync(context, cancellationToken);
}