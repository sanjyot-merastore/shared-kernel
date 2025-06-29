namespace MeraStore.Shared.Kernel.Core.Domain.Entities;

public interface IAuditable
{
    string CreatedBy { get; set; }
    DateTime CreatedDate { get; set; }

    string ModifiedBy { get; set; }
    DateTime? ModifiedDate { get; set; }
}

[Serializable]
public abstract class AuditableEntity(string createdBy) : Entity, IAuditable
{
    public string CreatedBy { get; set; } = string.IsNullOrWhiteSpace(createdBy) ? "System" : createdBy;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }

    // EF and deserializers love this guy
    protected AuditableEntity() : this("System") { }

    public void SetModifiedInfo(string modifiedBy)
    {
        if (string.IsNullOrWhiteSpace(modifiedBy))
        {
            throw new ArgumentException("ModifiedBy cannot be null or empty", nameof(modifiedBy));
        }

        ModifiedBy = modifiedBy;
        ModifiedDate = DateTime.UtcNow;
    }

    public override string ToString() =>
        $"{base.ToString()}, CreatedBy={CreatedBy}, CreatedDate={CreatedDate:u}";
}