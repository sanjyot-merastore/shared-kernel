using MeraStore.Shared.Kernel.Core.Domain.Entities;

namespace MeraStore.Shared.Kernel.Core.Events;

public abstract class AggregateRoot : Entity
{
  protected AggregateRoot() { }

  protected AggregateRoot(Ulid id) : base(id) { }
}