using Microsoft.Extensions.Logging;

namespace GSalvi.EventSourcing;

internal class EventStoreManager : EventStoreManager<EventData>
{
    public EventStoreManager(
        IEventDataRepository<EventData> repository,
        IEventDataBuilder<EventData> builder,
        ILogger<EventStoreManager> logger)
        : base(repository, builder, logger)
    {
    }
}

internal class EventStoreManager<TEventData> : IEventStoreManager
    where TEventData : EventData
{
    private readonly IEventDataRepository<TEventData> _repository;
    private readonly IEventDataBuilder<TEventData> _builder;
    private readonly ILogger<EventStoreManager<TEventData>> _logger;

    public EventStoreManager(
        IEventDataRepository<TEventData> repository,
        IEventDataBuilder<TEventData> builder,
        ILogger<EventStoreManager<TEventData>> logger)
    {
        _repository = repository;
        _builder = builder;
        _logger = logger;
    }

    public async Task StoreAsync(dynamic entity, Guid aggregateId, string eventType)
    {
        _logger.LogInformation("Event received with type {EventType} and aggregate id {AggregateId}",
            eventType, aggregateId.ToString());

        var eventData = await _builder.BuildAsync(aggregateId, eventType, entity);
        await _repository.AddAsync(eventData);

        _logger.LogInformation(
            "EventData with event type {EventType} and id {Id} has been addeded to database",
            eventType, ((EventData) eventData).Id.ToString());
    }
}