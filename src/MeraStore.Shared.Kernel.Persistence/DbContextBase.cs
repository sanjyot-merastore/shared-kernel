using System.Linq.Expressions;
using MeraStore.Shared.Kernel.Core.Domain.Entities;

namespace MeraStore.Shared.Kernel.Persistence;

/// <summary>
/// Base class for Entity Framework Core DbContext with built-in support for audit info and soft deletion.
/// </summary>
[ExcludeFromCodeCoverage]
public abstract class DbContextBase(DbContextOptions options) : DbContext(options)
{
  /// <summary>
  /// Overrides SaveChangesAsync to apply audit and soft delete information before saving changes to the database.
  /// </summary>
  /// <param name="cancellationToken">The cancellation token to observe while saving changes.</param>
  /// <returns>Returns the number of state entries written to the database.</returns>
  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    ApplyAuditInfo();
    ApplySoftDeletableInfo();

    // Optionally dispatch domain events here
    return await base.SaveChangesAsync(cancellationToken);
  }

  /// <summary>
  /// Configures the model during the model creation process to apply global filters, such as for soft deletions.
  /// </summary>
  /// <param name="modelBuilder">The model builder used to configure the model.</param>
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    ApplyGlobalFilters(modelBuilder);
  }

  /// <summary>
  /// Applies audit information to entities that implement IAuditable.
  /// Sets the CreatedDate for added entities and ModifiedDate for modified entities.
  /// </summary>
  private void ApplyAuditInfo()
  {
    var entries = ChangeTracker.Entries<IAuditable>();

    var now = DateTime.UtcNow;
    foreach (var entry in entries)
    {
      if (entry.State == EntityState.Added)
      {
        entry.Entity.CreatedDate = now;
      }

      if (entry.State == EntityState.Modified)
      {
        entry.Entity.ModifiedDate = now;
      }
    }
  }

  /// <summary>
  /// Applies soft deletion information to entities that implement ISoftDeletable.
  /// Marks the entity with a DeletedDate when it is deleted, instead of actually removing it from the database.
  /// </summary>
  private void ApplySoftDeletableInfo()
  {
    var deletable = ChangeTracker.Entries<ISoftDeletable>();

    var now = DateTime.UtcNow;
    foreach (var entry in deletable)
    {
      if (entry.State == EntityState.Deleted)
      {
        entry.Entity.DeletedDate = now;
      }
    }
  }

  /// <summary>
  /// Applies global filters to entities, including a soft delete filter to exclude soft-deleted entities from queries.
  /// </summary>
  /// <param name="modelBuilder">The model builder used to configure the model.</param>
  private void ApplyGlobalFilters(ModelBuilder modelBuilder)
  {
    // Apply soft delete filter for all ISoftDeletable entities
    foreach (var entityType in modelBuilder.Model.GetEntityTypes())
    {
      if (!typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType)) continue;

      var parameter = Expression.Parameter(entityType.ClrType, "e");
      var deletedCheck = Expression.Lambda(
        Expression.Equal(
          Expression.Property(parameter, nameof(ISoftDeletable.IsDeleted)),
          Expression.Constant(false)
        ), parameter);

      modelBuilder.Entity(entityType.ClrType).HasQueryFilter(deletedCheck);
    }
  }
}
