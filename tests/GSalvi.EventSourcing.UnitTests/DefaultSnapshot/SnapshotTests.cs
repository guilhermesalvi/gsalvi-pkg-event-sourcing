using System;
using AutoFixture;
using FluentAssertions;
using Xunit;

namespace GSalvi.EventSourcing.UnitTests.DefaultSnapshot
{
    public class SnapshotTests
    {
        private readonly Guid _id;
        private readonly Guid _aggregateId;
        private readonly string _eventType;
        private readonly string _serializedData;
        private readonly DateTime _timestamp;
        private readonly Snapshot _snapshot;

        public SnapshotTests()
        {
            var fixture = new Fixture();
            _id = fixture.Create<Guid>();
            _aggregateId = fixture.Create<Guid>();
            _eventType = fixture.Create<string>();
            _serializedData = fixture.Create<string>();
            _timestamp = fixture.Create<DateTime>();
            _snapshot = new Snapshot
            {
                Id = _id,
                AggregateId = _aggregateId,
                EventType = _eventType,
                SerializedData = _serializedData,
                Timestamp = _timestamp
            };
        }

        [Fact]
        public void Id_ShoulNotBeChanged_AfterInitialization()
        {
            // Act
            _snapshot.Id.Should().Be(_id);
        }

        [Fact]
        public void AggregateId_ShoulNotBeChanged_AfterInitialization()
        {
            // Act
            _snapshot.AggregateId.Should().Be(_aggregateId);
        }

        [Fact]
        public void EventType_ShoulNotBeChanged_AfterInitialization()
        {
            // Act
            _snapshot.EventType.Should().Be(_eventType);
        }

        [Fact]
        public void SerializedData_ShoulNotBeChanged_AfterInitialization()
        {
            // Act
            _snapshot.SerializedData.Should().Be(_serializedData);
        }

        [Fact]
        public void Timestamp_ShoulNotBeChanged_AfterInitialization()
        {
            // Act
            _snapshot.Timestamp.Should().Be(_timestamp);
        }
    }
}