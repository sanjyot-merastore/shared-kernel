namespace MeraStore.Shared.Kernel.Messaging.Kafka.Models;

public class MessageMetadata
{
    public string Topic { get; set; }
    public int Partition { get; set; }
    public long Offset { get; set; }
    public DateTime TimestampUtc { get; set; } // optional use for diagnostics
}