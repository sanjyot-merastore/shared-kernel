namespace MeraStore.Shared.Kernel.Common.Core.Domain.Entities;

public abstract class AuditableEntity(string createdBy) : Entity
{
  public DateTime CreatedDate { get; private set; } = DateTime.UtcNow;
  public DateTime? ModifiedDate { get; private set; }
  public string CreatedBy { get; private set; } = createdBy ?? throw new ArgumentNullException(nameof(createdBy));
  public string? ModifiedBy { get; private set; }

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