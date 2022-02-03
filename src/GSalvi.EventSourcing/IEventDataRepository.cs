namespace GSalvi.EventSourcing;

/// <summary>
/// Represents a repository to <typeparamref name="TEventData"/>
/// </summary>
/// <typeparam name="TEventData"></typeparam>
public interface IEventDataRepository<TEventData> where TEventData : EventData
{
    /// <summary>
    /// Adds a new <typeparamref name="TEventData"/> to repository
    /// </summary>
    /// <param name="eventData"></param>
    /// <returns></returns>
    Task AddAsync(TEventData eventData);

    /// <summary>
    /// Returns a <typeparamref name="TEventData"/> from its identifier
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TEventData> GetByIdAsync(Guid id);

    /// <summary>
    /// Returns a collection of <typeparamref name="TEventData"/> from its aggregate identifier
    /// </summary>
    /// <param name="aggregateId"></param>
    /// <returns></returns>
    Task<IEnumerable<TEventData>> GetByAggregateIdAsync(Guid aggregateId);

    /// <summary>
    /// Returns a collection <typeparamref name="TEventData"/> from its event type
    /// </summary>
    /// <param name="eventType"></param>
    /// <returns></returns>
    Task<IEnumerable<TEventData>> GetByEventTypeAsync(string eventType);

    /// <summary>
    /// Returns a query functionality to <typeparamref name="TEventData"/>
    /// </summary>
    /// <returns></returns>
    IQueryable<TEventData> GetAll();
}