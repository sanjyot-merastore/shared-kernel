namespace MeraStore.Shared.Kernel.Common.Core.Domain.Entities;

public interface ISoftDeletable
{
  bool IsDeleted { get; }
  DateTime? DeletedAt { get; }
}

public abstract class SoftDeletable : ISoftDeletable
{
  public bool IsDeleted { get; protected set; }
  public DateTime? DeletedAt { get; protected set; }
}