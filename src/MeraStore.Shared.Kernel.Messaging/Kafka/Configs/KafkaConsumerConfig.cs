namespace MeraStore.Shared.Kernel.Messaging.Kafka.Configs;

public class KafkaConsumerConfig
{
    public string ConsumerName { get; set; }
    public IDictionary<string, string> Settings { get; set; } = new Dictionary<string, string>();
    public string ConsumerTopics { get; set; } = string.Empty;
    public bool UseAvro { get; set; } = false; // Ignored for now
}