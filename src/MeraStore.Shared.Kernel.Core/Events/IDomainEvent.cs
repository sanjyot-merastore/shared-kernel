namespace MeraStore.Shared.Kernel.Core.Events;

public interface IDomainEvent
{
  public Guid Id { get; }
  public DateTime OccurredOn { get; } 
}