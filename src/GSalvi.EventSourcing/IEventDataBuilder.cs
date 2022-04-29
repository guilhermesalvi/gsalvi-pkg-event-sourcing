namespace GSalvi.EventSourcing;

/// <summary>
/// Defines a builder for <typeparamref name="TEventData"/>
/// </summary>
/// <typeparam name="TEventData"></typeparam>
public interface IEventDataBuilder<TEventData> where TEventData : EventData
{
    /// <summary>
    /// Returns a new instance of <typeparamref name="TEventData"/>
    /// </summary>
    /// <param name="aggregateId"></param>
    /// <param name="eventType"></param>
    /// <param name="entity"></param>
    /// <param name="additionalParams"></param>
    /// <returns></returns>
    Task<TEventData> BuildAsync(Guid aggregateId, string eventType, dynamic entity,
        params KeyValuePair<string, string>[] additionalParams);
}