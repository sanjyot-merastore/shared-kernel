namespace MeraStore.Shared.Kernel.Common.Core.Events;

public interface IEventPublisher
{
  Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : EventBase;
}