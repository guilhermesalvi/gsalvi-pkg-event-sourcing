using System;

namespace GSalvi.EventSourcing.IntegrationTests.Configurations
{
    public class MySnapshot : Snapshot
    {
        public MySnapshot(
            Guid id,
            Guid aggregateId,
            string eventType,
            string serializedData,
            DateTime timestamp)
            : base(id, aggregateId, eventType, serializedData, timestamp)
        {
        }
    }
}