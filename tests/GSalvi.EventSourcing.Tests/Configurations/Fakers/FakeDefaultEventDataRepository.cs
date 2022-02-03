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
    private readonly List<EventData> _data;

    public FakeDefaultEventDataRepository()
    {
        var fixture = new Fixture();

        var eventData1 = fixture
            .Build<EventData>()
            .With(x => x.AggregateId, new Guid("3c8e2dc0-68bd-4a1f-baa9-bbe675d4a63b"))
            .With(x => x.EventType, nameof(CustomerRegistered))
            .With(x => x.Timestamp, DateTime.UtcNow)
            .With(x => x.Entity,
                fixture.Build<CustomerRegistered>()
                    .With(x => x.CustomerId, new Guid("3c8e2dc0-68bd-4a1f-baa9-bbe675d4a63b"))
                    .Create())
            .Create();
        var eventData2 = fixture
            .Build<EventData>()
            .With(x => x.AggregateId, new Guid("12d79f9c-e51b-46eb-b42d-e0b459e7a496"))
            .With(x => x.EventType, nameof(CustomerRegistered))
            .With(x => x.Timestamp, DateTime.UtcNow)
            .With(x => x.Entity,
                fixture.Build<CustomerRegistered>()
                    .With(x => x.CustomerId, new Guid("12d79f9c-e51b-46eb-b42d-e0b459e7a496"))
                    .Create())
            .Create();

        _data = new List<EventData>
        {
            eventData1,
            eventData2
        };
    }

    public Task AddAsync(EventData eventData)
    {
        _data.Add(eventData);
        return Task.CompletedTask;
    }

    public Task<EventData> GetByIdAsync(Guid id) => throw new NotImplementedException();

    public Task<IEnumerable<EventData>> GetByAggregateIdAsync(Guid aggregateId) =>
        Task.FromResult(_data.Where(x => x.AggregateId == aggregateId));

    public Task<IEnumerable<EventData>> GetByEventTypeAsync(string eventType) =>
        throw new NotImplementedException();

    public IQueryable<EventData> GetAll() => throw new NotImplementedException();
}