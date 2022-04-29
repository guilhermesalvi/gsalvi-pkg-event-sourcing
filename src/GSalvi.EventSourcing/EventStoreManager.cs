namespace GSalvi.EventSourcing;

internal class EventStoreManager : EventStoreManager<EventData>
{
    public EventStoreManager(
        IEventDataRepository<EventData> repository,
        IEventDataBuilder<EventData> builder)
        : base(repository, builder)
    {
    }
}

internal class EventStoreManager<TEventData> : IEventStoreManager
    where TEventData : EventData
{
    private readonly IEventDataRepository<TEventData> _repository;
    private readonly IEventDataBuilder<TEventData> _builder;

    public EventStoreManager(
        IEventDataRepository<TEventData> repository,
        IEventDataBuilder<TEventData> builder)
    {
        _repository = repository;
        _builder = builder;
    }

    public async Task StoreAsync<TEntity>(TEntity entity, Guid aggregateId,
        params KeyValuePair<string, string>[] additionalParams)
        where TEntity : class
    {
        var eventType = typeof(TEntity).Name;
        var eventData = await _builder.BuildAsync(aggregateId, eventType, entity, additionalParams);

        await _repository.AddAsync(eventData);
    }
}