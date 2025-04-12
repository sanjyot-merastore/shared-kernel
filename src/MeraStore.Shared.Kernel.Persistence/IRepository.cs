using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore.Query;

namespace MeraStore.Shared.Kernel.Persistence
{
  /// <summary>
  /// Defines the repository contract with CRUD operations for an entity.
  /// </summary>
  /// <typeparam name="T">The type of the entity.</typeparam>
  public interface IRepository<T> where T : class
  {
    /// <summary>
    /// Retrieves an entity by its identifier asynchronously.
    /// </summary>
    /// <param name="id">The identifier of the entity.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The entity if found, otherwise null.</returns>
    Task<T?> GetByIdAsync(Ulid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves an entity by its identifier asynchronously.
    /// </summary>
    /// <typeparam name="TId">The type of the identifier.</typeparam>
    /// <param name="id">The identifier of the entity.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The entity if found, otherwise null.</returns>
    Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the first entity that matches the given predicate asynchronously.
    /// </summary>
    /// <param name="predicate">The predicate to filter the entity.</param>
    /// <param name="include">The related entities to include.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The entity if found, otherwise null.</returns>
    Task<T?> GetFirstOrDefaultAsync(
        Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a list of entities that match the given predicate asynchronously.
    /// </summary>
    /// <param name="predicate">The predicate to filter the entities.</param>
    /// <param name="orderBy">The ordering function for the results.</param>
    /// <param name="include">The related entities to include.</param>
    /// <param name="disableTracking">Whether to disable change tracking.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of matching entities.</returns>
    Task<IReadOnlyList<T>> GetAsync(
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        bool disableTracking = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all entities from the database asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of all entities.</returns>
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new entity to the database asynchronously.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The added entity.</returns>
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a range of new entities to the database asynchronously.
    /// </summary>
    /// <param name="entities">The entities to add.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The list of added entities.</returns>
    Task<IReadOnlyList<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing entity in the database asynchronously.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an entity from the database asynchronously.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes entities matching the given criteria asynchronously.
    /// </summary>
    /// <param name="criteria">The criteria to match entities for deletion.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of entities deleted.</returns>
    Task<int> DeleteAsync(Expression<Func<T, bool>> criteria, CancellationToken cancellationToken = default);
  }
}
