using MeraStore.Shared.Kernel.Core.Events;

namespace MeraStore.Shared.Kernel.Core.Domain.Entities;

public interface IEntity
{
  Ulid Id { get; set; }
}

public abstract class Entity : IEntity
{
  private readonly List<IDomainEvent> _domainEvents = [];

  public Ulid Id { get; set; } = Ulid.NewUlid();


  public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

  protected Entity() { }

  protected Entity(Ulid id) => Id = id;

  protected void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

  public void ClearDomainEvents() => _domainEvents.Clear();
}