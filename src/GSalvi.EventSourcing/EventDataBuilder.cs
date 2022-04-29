namespace GSalvi.EventSourcing;

internal class EventDataBuilder : IEventDataBuilder<EventData>
{
    public Task<EventData> BuildAsync(Guid aggregateId, string eventType, dynamic entity,
        params KeyValuePair<string, string>[] additionalParams)
    {
        var id = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        return Task.FromResult(new EventData
        {
            Id = id,
            AggregateId = aggregateId,
            Entity = entity,
            EventType = eventType,
            Timestamp = timestamp
        });
    }
}