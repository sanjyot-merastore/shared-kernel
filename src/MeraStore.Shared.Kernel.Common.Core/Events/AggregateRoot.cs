using MeraStore.Shared.Kernel.Common.Core.Domain.Entities;

namespace MeraStore.Shared.Kernel.Common.Core.Events;

public abstract class AggregateRoot : Entity
{
  protected AggregateRoot() { }

  protected AggregateRoot(Ulid id) : base(id) { }
}