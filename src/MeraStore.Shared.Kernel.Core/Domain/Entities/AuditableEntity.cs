namespace MeraStore.Shared.Kernel.Core.Domain.Entities;

public interface IAuditable
{
  string CreatedBy { get; set; }
  DateTime CreatedDate { get; set; }

  string ModifiedBy { get; set; }
  DateTime? ModifiedDate { get; set; }
}

public abstract class AuditableEntity(string createdBy) : Entity, IAuditable
{
  public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
  public DateTime? ModifiedDate { get; set; }
  public string CreatedBy { get; set; } = createdBy;
  public string ModifiedBy { get; set; }

  protected AuditableEntity() : this("System") { } // Defaulting to "System" if no user is specified

  public void SetModifiedInfo(string modifiedBy)
  {
    if (string.IsNullOrWhiteSpace(modifiedBy))
    {
      throw new ArgumentException("ModifiedBy cannot be empty", nameof(modifiedBy));
    }

    ModifiedBy = modifiedBy;
    ModifiedDate = DateTime.UtcNow;
  }
}