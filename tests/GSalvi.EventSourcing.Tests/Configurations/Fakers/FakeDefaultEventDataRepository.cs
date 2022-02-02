using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using GSalvi.EventSourcing.Tests.Configurations.Models;

namespace GSalvi.EventSourcing.Tests.Configurations.Fakers;

[ExcludeFromCodeCoverage]
public class FakeDefaultEventDataRepository : IEventDataRepository<EventData>
{
    private readonly List<EventData> _data = new();

    public FakeDefaultEventDataRepository()
    {
        var fixture = new Fixture();
        var aggregateId = new Guid("3c8e2dc0-68bd-4a1f-baa9-bbe675d4a63b");
        const string eventType = nameof(CustomerRegistered);
        var timestamp = fixture.Create<DateTime>();

        var eventData1 = fixture
            .Build<EventData>()
            .With(x => x.AggregateId, aggregateId)
            .With(x => x.EventType, eventType)
            .With(x => x.Timestamp, timestamp)
            .Create();
        var eventData2 = fixture
            .Build<EventData>()
            .With(x => x.AggregateId, aggregateId)
            .With(x => x.EventType, eventType)
            .With(x => x.Timestamp, timestamp)
            .Create();
        var eventData3 = fixture
            .Build<EventData>()
            .With(x => x.AggregateId, aggregateId)
            .With(x => x.EventType, eventType)
            .With(x => x.Timestamp, timestamp)
            .Create();

        _data.Add(eventData1);
        _data.Add(eventData2);
        _data.Add(eventData3);
    }

    public Task AddAsync(EventData eventData)
    {
        _data.Add(eventData);
        return Task.CompletedTask;
    }

    public Task<EventData> GetByIdAsync(Guid id) => throw new NotImplementedException();

    public Task<IEnumerable<EventData>> GetByAggregateIdAsync(Guid aggregateId) =>
        Task.FromResult(_data.Where(x => x.AggregateId == aggregateId))!;

    public Task<IEnumerable<EventData>> GetByEventTypeAsync(string eventType) =>
        throw new NotImplementedException();

    public IQueryable<EventData> GetAll() => throw new NotImplementedException();
}