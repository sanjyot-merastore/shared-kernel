using MeraStore.Shared.Kernel.Messaging.Kafka.Abstraction;

namespace MeraStore.Shared.Kernel.Messaging.Kafka;

public class MessageProcessorItem<T>(IMessageProcessor<T> processor) where T : class
{
    public IMessageProcessor<T> Processor { get; init; } = processor;
}