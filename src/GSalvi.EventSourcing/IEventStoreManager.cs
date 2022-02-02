namespace GSalvi.EventSourcing;

public interface IEventStoreManager
{
    Task StoreAsync(dynamic entity, Guid aggregateId, string eventType);
}