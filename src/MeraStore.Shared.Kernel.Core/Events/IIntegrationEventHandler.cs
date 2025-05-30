namespace MeraStore.Shared.Kernel.Core.Events;

public interface IIntegrationEventHandler<in TEvent> where TEvent : IntegrationEvent
{
  Task Handle(TEvent integrationEvent, CancellationToken cancellationToken);
}