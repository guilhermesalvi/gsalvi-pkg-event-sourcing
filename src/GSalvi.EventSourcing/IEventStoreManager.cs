namespace GSalvi.EventSourcing;

/// <summary>
/// Defines a manager of events
/// </summary>
public interface IEventStoreManager
{
    /// <summary>
    /// Saves a new event in the repository
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="aggregateId"></param>
    /// <param name="eventType"></param>
    /// <returns></returns>
    Task StoreAsync(dynamic entity, Guid aggregateId, string eventType);
}