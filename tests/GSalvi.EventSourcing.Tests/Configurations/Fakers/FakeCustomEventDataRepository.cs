using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using GSalvi.EventSourcing.Tests.Configurations.Models;

namespace GSalvi.EventSourcing.Tests.Configurations.Fakers;

[ExcludeFromCodeCoverage]
public class FakeCustomEventDataRepository : IEventDataRepository<CustomEventData>
{
    private readonly List<CustomEventData> _data;

    public FakeCustomEventDataRepository()
    {
        var fixture = new Fixture();

        var eventData1 = fixture
            .Build<CustomEventData>()
            .With(x => x.AggregateId, new Guid("3c8e2dc0-68bd-4a1f-baa9-bbe675d4a63b"))
            .With(x => x.EventType, nameof(CustomerRegistered))
            .With(x => x.Timestamp, DateTime.UtcNow)
            .With(x => x.UserId, new Guid("e872aa96-2c62-4a99-9bb5-fcdcd0fb093e"))
            .With(x => x.Entity,
                fixture.Build<CustomerRegistered>()
                    .With(x => x.CustomerId, new Guid("3c8e2dc0-68bd-4a1f-baa9-bbe675d4a63b"))
                    .Create())
            .Create();
        var eventData2 = fixture
            .Build<CustomEventData>()
            .With(x => x.AggregateId, new Guid("12d79f9c-e51b-46eb-b42d-e0b459e7a496"))
            .With(x => x.EventType, nameof(CustomerRegistered))
            .With(x => x.Timestamp, DateTime.UtcNow)
            .With(x => x.UserId, new Guid("e872aa96-2c62-4a99-9bb5-fcdcd0fb093e"))
            .With(x => x.Entity,
                fixture.Build<CustomerRegistered>()
                    .With(x => x.CustomerId, new Guid("12d79f9c-e51b-46eb-b42d-e0b459e7a496"))
                    .Create())
            .Create();

        _data = new List<CustomEventData>
        {
            eventData1,
            eventData2
        };
    }

    public Task AddAsync(CustomEventData eventData)
    {
        _data.Add(eventData);
        return Task.CompletedTask;
    }

    public Task<CustomEventData> GetByIdAsync(Guid id) => throw new NotImplementedException();

    public Task<IEnumerable<CustomEventData>> GetByAggregateIdAsync(Guid aggregateId) =>
        Task.FromResult(_data.Where(x => x.AggregateId == aggregateId));

    public Task<IEnumerable<CustomEventData>> GetByEventTypeAsync(string eventType) =>
        throw new NotImplementedException();

    public IQueryable<CustomEventData> GetAll() => throw new NotImplementedException();
}