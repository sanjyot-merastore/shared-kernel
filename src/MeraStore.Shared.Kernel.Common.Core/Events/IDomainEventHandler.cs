namespace MeraStore.Shared.Kernel.Common.Core.Events;

public interface IDomainEventHandler<in TEvent> where TEvent : IDomainEvent
{
  Task Handle(TEvent domainEvent, CancellationToken cancellationToken);
}