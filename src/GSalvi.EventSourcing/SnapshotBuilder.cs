using System;

namespace GSalvi.EventSourcing
{
    internal class SnapshotBuilder : ISnapshotBuilder
    {
        public Snapshot Create(
            Guid aggregateId,
            string eventType,
            string serializedData)
        {
            var id = Guid.NewGuid();
            var timestamp = DateTime.UtcNow;

            return new Snapshot
            {
                Id = id,
                AggregateId = aggregateId,
                EventType = eventType,
                SerializedData = serializedData,
                Timestamp = timestamp
            };
        }
    }
}