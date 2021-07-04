using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using GSalvi.EventSourcing.IntegrationTests.Configurations;

namespace GSalvi.EventSourcing.IntegrationTests.Fakers
{
    public class FakeSnapshotRepository : ISnapshotRepository<MySnapshot>
    {
        public readonly List<MySnapshot> Data;

        public FakeSnapshotRepository()
        {
            var fixture = new Fixture();
            var aggregateId = new Guid("3c8e2dc0-68bd-4a1f-baa9-bbe675d4a63b");
            var eventType = "MyDummyEvent";
            var serializedData = fixture.Create<string>();
            var timestamp = fixture.Create<DateTime>();

            var snapshot1 = fixture
                .Build<MySnapshot>()
                .With(x => x.AggregateId, aggregateId)
                .With(x => x.EventType, eventType)
                .With(x => x.SerializedData, serializedData)
                .With(x => x.Timestamp, timestamp)
                .Create();
            var snapshot2 = fixture
                .Build<MySnapshot>()
                .With(x => x.AggregateId, aggregateId)
                .With(x => x.EventType, eventType)
                .With(x => x.SerializedData, serializedData)
                .With(x => x.Timestamp, timestamp)
                .Create();
            var snapshot3 = fixture
                .Build<MySnapshot>()
                .With(x => x.SerializedData, serializedData)
                .With(x => x.Timestamp, timestamp)
                .Create();
            Data = new List<MySnapshot> {snapshot1, snapshot2, snapshot3};
        }

        public Task AddAsync(MySnapshot obj)
        {
            Data.Add(obj);
            return Task.CompletedTask;
        }

        public Task<MySnapshot> GetByIdAsync(Guid id)
        {
            return Task.FromResult(Data.FirstOrDefault(x => x.Id == id));
        }

        public Task<IEnumerable<MySnapshot>> GetByAggregateIdAsync(Guid aggregateId)
        {
            return Task.FromResult(Data.Where(x => x.AggregateId == aggregateId));
        }

        public Task<IEnumerable<MySnapshot>> GetByEventTypeAsync(string eventType)
        {
            return Task.FromResult(Data.Where(x => x.EventType == eventType));
        }

        public IQueryable<MySnapshot> GetAll()
        {
            return Data.AsQueryable();
        }
    }
}