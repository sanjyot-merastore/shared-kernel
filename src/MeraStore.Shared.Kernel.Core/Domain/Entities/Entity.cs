using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

using MeraStore.Shared.Kernel.Core.Events;

namespace MeraStore.Shared.Kernel.Core.Domain.Entities;

public interface IEntity
{
    string Id { get; set; }

    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

    void ClearDomainEvents();
}

[Serializable]
public abstract class Entity : IEntity
{
    private readonly List<IDomainEvent> _domainEvents = [];

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)] // We generate ULID ourselves
    public string Id { get; set; } = Ulid.NewUlid().ToString();

    [JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected Entity() { }

    protected Entity(Ulid id) => Id = id.ToString();

    protected Entity(string id) => Id = id;

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        if (domainEvent is not null)
            _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents() => _domainEvents.Clear();

    public override string ToString() => $"{GetType().Name} [Id={Id}]";
}