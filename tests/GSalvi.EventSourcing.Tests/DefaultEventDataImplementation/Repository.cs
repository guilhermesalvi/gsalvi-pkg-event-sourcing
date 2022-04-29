using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Mongo2Go;
using MongoDB.Driver;

namespace GSalvi.EventSourcing.Tests.DefaultEventDataImplementation;

[ExcludeFromCodeCoverage]
public class Repository : IEventDataRepository<EventData>
{
    private readonly IMongoCollection<EventData> _collection;

    public Repository(MongoDbRunner runner)
    {
        var client = new MongoClient(runner.ConnectionString);
        var database = client.GetDatabase("eventDataDatabase");
        _collection = database.GetCollection<EventData>("eventData");
    }

    public Task AddAsync(EventData eventData) => _collection.InsertOneAsync(eventData);

    public Task<EventData> GetByIdAsync(Guid id) =>
        _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<IEnumerable<EventData>> GetByAggregateIdAsync(Guid aggregateId)
    {
        return await _collection.Find(x => x.AggregateId == aggregateId).ToListAsync();
    }

    public async Task<IEnumerable<EventData>> GetByEventTypeAsync(string eventType)
    {
        return await _collection.Find(x => x.EventType == eventType).ToListAsync();
    }

    public IQueryable<EventData> GetAll() => _collection.AsQueryable();
}