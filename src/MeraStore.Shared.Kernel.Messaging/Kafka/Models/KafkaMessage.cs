namespace MeraStore.Shared.Kernel.Messaging.Kafka.Models;

public class KafkaMessage<TValue> where TValue : class
{
    public string Key { get; set; }
    public TValue Value { get; set; }
    public IDictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
    public bool ForceCommit { get; set; } // not used, reserved
}