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
    /// <param name="additionalParams"></param>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    Task StoreAsync<TEntity>(TEntity entity, Guid aggregateId, params KeyValuePair<string, string>[] additionalParams)
        where TEntity : class;
}