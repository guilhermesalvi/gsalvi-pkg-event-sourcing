using System;
using GSalvi.EventSourcing.IntegrationTests.Configurations;

namespace GSalvi.EventSourcing.IntegrationTests.Fakers
{
    public class FakeSnapshotBuilder : ISnapshotBuilder<MySnapshot>
    {
        public MySnapshot Create(
            Guid aggregateId,
            string eventType,
            string serializedData)
        {
            var id = Guid.NewGuid();
            var timestamp = DateTime.UtcNow;

            return new MySnapshot
            {
                Id = id,
                AggregateId = aggregateId,
                EventType = eventType,
                SerializedData = serializedData,
                Timestamp = timestamp,
                UserId = string.Empty
            };
        }
    }
}