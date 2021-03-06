namespace GSalvi.EventSourcing;

/// <summary>
/// Defines an event data
/// </summary>
public class EventData
{
    /// <summary>
    /// Represents the identifier of event data
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Represents the identifier of aggregation
    /// </summary>
    public Guid AggregateId { get; set; }

    /// <summary>
    /// Represents the type of event
    /// </summary>
    public string? EventType { get; set; }

    /// <summary>
    /// Represents the event object
    /// </summary>
    public dynamic? Entity { get; set; }

    /// <summary>
    /// Represents the timestamp that the event occurred
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Returns a parsed <typeparamref name="TEntity"/> from <see cref="Entity"/> property
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    public TEntity? ParsedEntity<TEntity>() where TEntity : class => Entity as TEntity;
}