using System;
using System.Diagnostics.CodeAnalysis;

namespace GSalvi.EventSourcing.UnitTests.Configurations
{
    [ExcludeFromCodeCoverage]
    public class MySnapshotBuilder : ISnapshotBuilder<MySnapshot>
    {
        public MySnapshot Create(Guid aggregateId, string eventType, string serializedData)
        {
            return new();
        }
    }
}