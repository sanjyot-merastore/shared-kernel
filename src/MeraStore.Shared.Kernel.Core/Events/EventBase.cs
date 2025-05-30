namespace MeraStore.Shared.Kernel.Core.Events;

public abstract class EventBase
{
  public Guid Id { get; } = Guid.NewGuid();
  public DateTime OccurredOn { get; } = DateTime.UtcNow;
}