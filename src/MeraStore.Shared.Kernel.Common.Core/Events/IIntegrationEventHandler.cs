namespace MeraStore.Shared.Kernel.Common.Core.Events;

public interface IIntegrationEventHandler<in TEvent> where TEvent : IntegrationEvent
{
  Task Handle(TEvent integrationEvent, CancellationToken cancellationToken);
}