namespace GSalvi.EventSourcing;

internal class EventDataBuilder : IEventDataBuilder
{
    public Task<EventData> BuildAsync(Guid aggregateId, string eventType, dynamic entity)
    {
        var id = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        return Task.FromResult(new EventData
        {
            Id = id,
            AggregateId = aggregateId,
            EventType = eventType,
            Entity = entity,
            Timestamp = timestamp
        });
    }
}