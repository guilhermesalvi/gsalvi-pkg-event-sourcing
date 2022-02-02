namespace GSalvi.EventSourcing;

public class EventData
{
    public Guid Id { get; set; }
    public Guid AggregateId { get; set; }
    public string? EventType { get; set; }
    public dynamic? Entity { get; set; }
    public DateTime Timestamp { get; set; }
}