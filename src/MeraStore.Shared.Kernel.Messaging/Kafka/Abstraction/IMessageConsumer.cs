using MeraStore.Shared.Kernel.Messaging.Kafka;
using MeraStore.Shared.Kernel.Messaging.Kafka.Models;

namespace MeraStore.Shared.Kernel.Messaging.Kafka.Abstraction;

public interface IMessageConsumer<T> : IDisposable where T : class
{
    void Start(int timeoutMs = 1000, string optionalTopics = "");
    Task Stop();
    void SetMessageProcessors(List<MessageProcessorItem<T>> processors);
    IEnumerable<MessageMetadata> Commit();
    void Commit(IEnumerable<MessageMetadata> offsets);
    string GetConfigName();
}