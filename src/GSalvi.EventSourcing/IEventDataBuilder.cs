namespace GSalvi.EventSourcing;

public interface IEventDataBuilder : IEventDataBuilder<EventData>
{
}

public interface IEventDataBuilder<TEventData> where TEventData : EventData
{
    Task<TEventData> BuildAsync(Guid aggregateId, string eventType, dynamic entity);
}