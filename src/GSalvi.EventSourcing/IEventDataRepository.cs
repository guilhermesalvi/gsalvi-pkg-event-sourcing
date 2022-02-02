namespace GSalvi.EventSourcing;

public interface IEventDataRepository<TEventData> where TEventData : EventData
{
    Task AddAsync(TEventData eventData);
    Task<TEventData> GetByIdAsync(Guid id);
    Task<IEnumerable<TEventData>> GetByAggregateIdAsync(Guid aggregateId);
    Task<IEnumerable<TEventData>> GetByEventTypeAsync(string eventType);
    IQueryable<TEventData> GetAll();
}