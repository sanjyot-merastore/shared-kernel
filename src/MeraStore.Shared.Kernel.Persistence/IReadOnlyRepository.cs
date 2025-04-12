using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace MeraStore.Shared.Kernel.Persistence;

/// <summary>
/// Defines the read-only repository contract with operations to retrieve entities.
/// </summary>
/// <typeparam name="T">The type of the entity.</typeparam>
public interface IReadOnlyRepository<T> where T : class
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
}