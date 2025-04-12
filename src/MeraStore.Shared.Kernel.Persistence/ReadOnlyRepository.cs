using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace MeraStore.Shared.Kernel.Persistence.EFCore;

public class ReadOnlyRepository<T>(DbContext dbContext) : IReadOnlyRepository<T> where T : class
{
  private readonly DbSet<T> _dbSet = dbContext.Set<T>();


  /// <summary>
  /// Retrieves all entities from the database asynchronously.
  /// </summary>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>A list of all entities.</returns>
  public virtual async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
      => await _dbSet.AsNoTracking().ToListAsync(cancellationToken);

  /// <summary>
  /// Retrieves an entity by its identifier asynchronously.
  /// </summary>
  /// <param name="id">The identifier of the entity.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>The entity if found, otherwise null.</returns>
  public virtual async Task<T?> GetByIdAsync(Ulid id, CancellationToken cancellationToken = default)
      => await _dbSet.FindAsync([id], cancellationToken);

  /// <summary>
  /// Retrieves an entity by its identifier asynchronously.
  /// </summary>
  /// <typeparam name="TId">The type of the identifier.</typeparam>
  /// <param name="id">The identifier of the entity.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>The entity if found, otherwise null.</returns>
  public async Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default)
      => await _dbSet.FindAsync([id], cancellationToken);

  /// <summary>
  /// Retrieves the first entity that matches the given predicate asynchronously.
  /// </summary>
  /// <param name="predicate">The predicate to filter the entity.</param>
  /// <param name="include">The related entities to include.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>The entity if found, otherwise null.</returns>
  public virtual async Task<T?> GetFirstOrDefaultAsync(
      Expression<Func<T, bool>> predicate,
      Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
      CancellationToken cancellationToken = default)
  {
    IQueryable<T> query = _dbSet;

    if (include != null)
      query = include(query);

    return await query.FirstOrDefaultAsync(predicate, cancellationToken);
  }

  /// <summary>
  /// Retrieves a list of entities that match the given predicate asynchronously.
  /// </summary>
  /// <param name="predicate">The predicate to filter the entities.</param>
  /// <param name="orderBy">The ordering function for the results.</param>
  /// <param name="include">The related entities to include.</param>
  /// <param name="disableTracking">Whether to disable change tracking.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>A list of matching entities.</returns>
  public virtual async Task<IReadOnlyList<T>> GetAsync(
      Expression<Func<T, bool>>? predicate = null,
      Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
      Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
      bool disableTracking = true,
      CancellationToken cancellationToken = default)
  {
    IQueryable<T> query = _dbSet;

    if (disableTracking)
      query = query.AsNoTracking();

    if (include != null)
      query = include(query);

    if (predicate != null)
      query = query.Where(predicate);

    if (orderBy != null)
      query = orderBy(query);

    return await query.ToListAsync(cancellationToken);
  }
}