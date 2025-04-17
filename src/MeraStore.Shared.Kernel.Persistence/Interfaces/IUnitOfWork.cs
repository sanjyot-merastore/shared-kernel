using Microsoft.EntityFrameworkCore;

namespace MeraStore.Shared.Kernel.Persistence.Interfaces
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
    Task<int> CommitAsync(DbContext context, CancellationToken cancellationToken = default);
  }
}
