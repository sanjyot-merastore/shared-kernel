using Confluent.Kafka;

namespace MeraStore.Shared.Kernel.Messaging.Kafka.Abstraction;

public interface IMessageProcessor<T> where T : class
{
    void BeforeProcessing(IMessageConsumer<T> consumer, Message<string, T> message);
    void Process(Message<string, T> message);
    void AfterProcessing(IMessageConsumer<T> consumer, Message<string, T> message);
}