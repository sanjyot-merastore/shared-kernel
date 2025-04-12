using Microsoft.EntityFrameworkCore;

namespace MeraStore.Shared.Kernel.Persistence
{
  /// <summary>
  /// Defines a contract for committing changes to the database.
  /// </summary>
  public interface IUnitOfWork
  {
    /// <summary>
    /// Asynchronously commits changes made in the current unit of work.
    /// </summary>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> CommitAsync(CancellationToken cancellationToken = default);
  }

  /// <summary>
  /// Represents the unit of work that coordinates the changes with the DbContext.
  /// </summary>
  public class UnitOfWork : IUnitOfWork
  {
    private readonly DbContext _context;
    private readonly ISaveChangesStrategy _saveChangesStrategy;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
    /// </summary>
    /// <param name="context">The DbContext to be used for saving changes.</param>
    /// <param name="saveChangesStrategy">The strategy for saving changes.</param>
    public UnitOfWork(DbContext context, ISaveChangesStrategy saveChangesStrategy)
    {
      _context = context;
      _saveChangesStrategy = saveChangesStrategy;
    }

    /// <summary>
    /// Commits the changes to the database using the specified save changes strategy.
    /// </summary>
    /// <returns>The number of state entries written to the database.</returns>
    public Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
      return _saveChangesStrategy.SaveChangesAsync(cancellationToken);
    }
  }

  /// <summary>
  /// Defines a contract for saving changes asynchronously.
  /// </summary>
  public interface ISaveChangesStrategy
  {
    /// <summary>
    /// Asynchronously saves the changes to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to observe during the operation.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
  }

  /// <summary>
  /// The default strategy for saving changes using the DbContext.
  /// </summary>
  public class DefaultSaveChangesStrategy : ISaveChangesStrategy
  {
    private readonly DbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultSaveChangesStrategy"/> class.
    /// </summary>
    /// <param name="dbContext">The DbContext instance to use for saving changes.</param>
    public DefaultSaveChangesStrategy(DbContext dbContext)
    {
      _dbContext = dbContext;
    }

    /// <summary>
    /// Asynchronously saves changes to the database using the DbContext.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to observe during the operation.</param>
    /// <returns>The number of state entries written to the database.</returns>
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
      return _dbContext.SaveChangesAsync(cancellationToken);
    }
  }

  /// <summary>
  /// A no-op strategy for saving changes, where the responsibility lies with the UnitOfWork.
  /// </summary>
  public class NoOpSaveChangesStrategy : ISaveChangesStrategy
  {
    /// <summary>
    /// Asynchronously does nothing (no-op) for saving changes.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to observe during the operation.</param>
    /// <returns>A completed task indicating no changes were made.</returns>
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
      // No-op: responsibility lies with UnitOfWork to handle saving
      return Task.FromResult(0);
    }
  }
}
