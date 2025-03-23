using MeraStore.Shared.Kernel.Common.Core.Events;

namespace MeraStore.Shared.Kernel.Common.Core.Domain.Entities;

public abstract class Entity
{
  private readonly List<IDomainEvent> _domainEvents = [];

  public Ulid Id { get; protected set; } = Ulid.NewUlid();
  public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

  protected Entity() { }

  protected Entity(Ulid id) => Id = id;

  protected void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

  public void ClearDomainEvents() => _domainEvents.Clear();
}