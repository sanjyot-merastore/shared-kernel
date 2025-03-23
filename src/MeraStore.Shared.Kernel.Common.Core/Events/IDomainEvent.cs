namespace MeraStore.Shared.Kernel.Common.Core.Events;

public interface IDomainEvent
{
  public Guid Id { get; }
  public DateTime OccurredOn { get; } 
}