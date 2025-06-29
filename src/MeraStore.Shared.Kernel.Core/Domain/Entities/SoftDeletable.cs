namespace MeraStore.Shared.Kernel.Core.Domain.Entities;

/// <summary>
/// Defines properties and operations for soft deletion behavior.
/// </summary>
public interface ISoftDeletable
{
    /// <summary>
    /// Gets or sets a value indicating whether the entity is marked as deleted.
    /// </summary>
    bool IsDeleted { get; set; }

    /// <summary>
    /// Gets or sets the UTC timestamp when the entity was soft deleted.
    /// </summary>
    DateTime? DeletedDate { get; set; }

    /// <summary>
    /// Marks the entity as deleted and sets the deletion timestamp.
    /// </summary>
    void SoftDelete();

    /// <summary>
    /// Reverts the soft deletion, marking the entity as active again.
    /// </summary>
    void UndoDelete();
}

/// <summary>
/// Base class providing reusable soft deletion behavior.
/// </summary>
public abstract class SoftDeletable : ISoftDeletable
{
    /// <inheritdoc />
    public bool IsDeleted { get; set; }

    /// <inheritdoc />
    public DateTime? DeletedDate { get; set; }

    /// <inheritdoc />
    public virtual void SoftDelete()
    {
        if (!IsDeleted)
        {
            IsDeleted = true;
            DeletedDate = DateTime.UtcNow;
        }
    }

    /// <inheritdoc />
    public virtual void UndoDelete()
    {
        if (IsDeleted)
        {
            IsDeleted = false;
            DeletedDate = null;
        }
    }

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    public override string ToString() =>
        $"IsDeleted={IsDeleted}, DeletedDate={DeletedDate:u}";
}

/// <summary>
/// A base entity that supports audit and soft deletion capabilities.
/// </summary>
public abstract class SoftDeletableEntity : AuditableEntity, ISoftDeletable
{
    /// <inheritdoc />
    public bool IsDeleted { get; set; }

    /// <inheritdoc />
    public DateTime? DeletedDate { get; set; }

    /// <inheritdoc />
    public void SoftDelete()
    {
        if (!IsDeleted)
        {
            IsDeleted = true;
            DeletedDate = DateTime.UtcNow;
        }
    }

    /// <inheritdoc />
    public void UndoDelete()
    {
        if (IsDeleted)
        {
            IsDeleted = false;
            DeletedDate = null;
        }
    }
}
