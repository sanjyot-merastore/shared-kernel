using System.Linq.Expressions;

using MeraStore.Shared.Kernel.Persistence.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace MeraStore.Shared.Kernel.Persistence.Repositories
{
  /// <summary>
  /// A repository that provides basic CRUD operations for an entity in EF Core.
  /// </summary>
  /// <typeparam name="T">The entity type.</typeparam>
  public class Repository<T>(DbContext context, ICommitStrategy commit) : IRepository<T>
    where T : class
  {
    private readonly DbSet<T> _dbSet = context.Set<T>();

    #region Read Operations

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

    #endregion

    #region Write Operations

    /// <summary>
    /// Adds a new entity to the database asynchronously.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The added entity.</returns>
    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
      await _dbSet.AddAsync(entity, cancellationToken);
      await commit.SaveChangesAsync(context, cancellationToken);
      return entity;
    }

    /// <summary>
    /// Adds a range of new entities to the database asynchronously.
    /// </summary>
    /// <param name="entities">The entities to add.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The list of added entities.</returns>
    public async Task<IReadOnlyList<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
      await _dbSet.AddRangeAsync(entities, cancellationToken);
      await commit.SaveChangesAsync(context, cancellationToken);
      return entities.ToList();
    }

    /// <summary>
    /// Updates an existing entity in the database asynchronously.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
      _dbSet.Update(entity);
      await commit.SaveChangesAsync(context, cancellationToken);
    }

    /// <summary>
    /// Deletes an entity from the database asynchronously.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
      _dbSet.Remove(entity);
      await commit.SaveChangesAsync(context, cancellationToken);
    }

    /// <summary>
    /// Deletes entities matching the given criteria asynchronously.
    /// </summary>
    /// <param name="criteria">The criteria to match entities for deletion.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of entities deleted.</returns>
    public async Task<int> DeleteAsync(Expression<Func<T, bool>> criteria, CancellationToken cancellationToken = default)
    {
      var entitiesToDelete = await _dbSet.Where(criteria).ToListAsync(cancellationToken);
      _dbSet.RemoveRange(entitiesToDelete);
      await commit.SaveChangesAsync(context, cancellationToken);
      return entitiesToDelete.Count;
    }

    #endregion
  }
}
