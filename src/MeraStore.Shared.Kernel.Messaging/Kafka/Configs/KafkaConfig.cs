namespace MeraStore.Shared.Kernel.Messaging.Kafka.Configs;

public class KafkaConfig
{
    public KafkaProducerConfig Producer { get; set; }
    public List<KafkaConsumerConfig> Consumers { get; set; } = new();
}