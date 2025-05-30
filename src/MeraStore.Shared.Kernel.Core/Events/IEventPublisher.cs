namespace MeraStore.Shared.Kernel.Core.Events;

public interface IEventPublisher
{
  Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : EventBase;
}