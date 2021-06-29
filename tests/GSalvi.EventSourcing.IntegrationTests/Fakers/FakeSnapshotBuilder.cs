using System;
using GSalvi.EventSourcing.IntegrationTests.Configurations;

namespace GSalvi.EventSourcing.IntegrationTests.Fakers
{
    public class FakeSnapshotBuilder : ISnapshotBuilder<MySnapshot>
    {
        public MySnapshot Create(
            Guid id,
            Guid aggregateId,
            string eventType,
            string serializedData,
            DateTime timestamp)
        {
            return new(id, aggregateId, eventType, serializedData, timestamp);
        }
    }
}