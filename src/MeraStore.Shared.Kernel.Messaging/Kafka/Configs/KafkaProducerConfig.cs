namespace MeraStore.Shared.Kernel.Messaging.Kafka.Configs;

public class KafkaProducerConfig
{
    public IDictionary<string, string> Settings { get; set; } = new Dictionary<string, string>();
    public bool AutoRegisterSchemas { get; set; } = false;
    public bool UseAvro { get; set; } = false; // Ignored for now
}