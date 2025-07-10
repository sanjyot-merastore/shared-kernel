using Confluent.Kafka;

using MeraStore.Shared.Kernel.Messaging.Kafka.Abstraction;

namespace MeraStore.Shared.Kernel.Messaging.Kafka;

public abstract class MessageProcessorBase<T> : IMessageProcessor<T> where T : class
{
    public abstract void BeforeProcessing(IMessageConsumer<T> consumer, Message<string, T> message);
    public abstract void Process(Message<string, T> message);
    public abstract void AfterProcessing(IMessageConsumer<T> consumer, Message<string, T> message);
}