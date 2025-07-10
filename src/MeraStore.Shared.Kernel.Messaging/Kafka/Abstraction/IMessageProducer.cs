using MeraStore.Shared.Kernel.Messaging.Kafka.Models;

namespace MeraStore.Shared.Kernel.Messaging.Kafka.Abstraction;

public interface IMessageProducer<T> where T : class
{
    Task ProduceAsync(KafkaMessage<T> message, string topic);
}
