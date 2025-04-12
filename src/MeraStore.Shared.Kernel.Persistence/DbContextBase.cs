using System.Linq.Expressions;
using MeraStore.Shared.Kernel.Common.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeraStore.Shared.Kernel.Persistence.EFCore;

public abstract class DbContextBase(DbContextOptions options) : DbContext(options)
{
  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    ApplyAuditInfo();
    // Optionally dispatch domain events here
    return await base.SaveChangesAsync(cancellationToken);
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    ApplyGlobalFilters(modelBuilder);
  }

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

  private void ApplyGlobalFilters(ModelBuilder modelBuilder)
  {
    // Apply soft delete filter for all ISoftDeletable entities
    foreach (var entityType in modelBuilder.Model.GetEntityTypes())
    {
      if (typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType))
      {
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
}