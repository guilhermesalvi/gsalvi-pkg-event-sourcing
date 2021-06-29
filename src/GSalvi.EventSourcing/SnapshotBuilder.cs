using System;

namespace GSalvi.EventSourcing
{
    internal class SnapshotBuilder : ISnapshotBuilder<Snapshot>
    {
        public Snapshot Create(
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