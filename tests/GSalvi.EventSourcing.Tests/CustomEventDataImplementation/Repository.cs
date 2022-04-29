using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Mongo2Go;
using MongoDB.Driver;

namespace GSalvi.EventSourcing.Tests.CustomEventDataImplementation;

[ExcludeFromCodeCoverage]
public class Repository : IEventDataRepository<CustomEventData>
{
    private readonly IMongoCollection<CustomEventData> _collection;

    public Repository(MongoDbRunner runner)
    {
        var client = new MongoClient(runner.ConnectionString);
        var database = client.GetDatabase("eventDataDatabase");
        _collection = database.GetCollection<CustomEventData>("customEventData");
    }

    public Task AddAsync(CustomEventData eventData) => _collection.InsertOneAsync(eventData);

    public Task<CustomEventData> GetByIdAsync(Guid id) =>
        _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<IEnumerable<CustomEventData>> GetByAggregateIdAsync(Guid aggregateId)
    {
        return await _collection.Find(x => x.AggregateId == aggregateId).ToListAsync();
    }

    public async Task<IEnumerable<CustomEventData>> GetByEventTypeAsync(string eventType)
    {
        return await _collection.Find(x => x.EventType == eventType).ToListAsync();
    }

    public IQueryable<CustomEventData> GetAll() => _collection.AsQueryable();
}