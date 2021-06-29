using System;
using System.Diagnostics.CodeAnalysis;

namespace GSalvi.EventSourcing.UnitTests.Configurations
{
    [ExcludeFromCodeCoverage]
    public class DummySnapshot : Snapshot
    {
        public string DummyProp { get; set; }

        public DummySnapshot(
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