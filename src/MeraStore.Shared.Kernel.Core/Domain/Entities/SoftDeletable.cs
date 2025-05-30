namespace MeraStore.Shared.Kernel.Core.Domain.Entities;

public interface ISoftDeletable
{
  bool IsDeleted { get; set; }
  DateTime? DeletedDate { get; set; }
}

public abstract class SoftDeletable : ISoftDeletable
{
  public bool IsDeleted { get; set; }
  public DateTime? DeletedDate { get; set; }
}