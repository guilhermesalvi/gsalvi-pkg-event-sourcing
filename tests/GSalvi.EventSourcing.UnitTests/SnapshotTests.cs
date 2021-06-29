using System;
using System.Diagnostics.CodeAnalysis;
using AutoFixture;
using FluentAssertions;
using Xunit;

namespace GSalvi.EventSourcing.UnitTests
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
            _snapshot = new Snapshot(_id, _aggregateId, _eventType, _serializedData, _timestamp);
        }

        [Fact]
        public void Id_ShouldNotBeChanged_AfterObjectInitialization()
        {
            _snapshot.Id.Should().Be(_id);
        }

        [Fact]
        public void AggregateId_ShouldNotBeChanged_AfterObjectInitialization()
        {
            _snapshot.AggregateId.Should().Be(_aggregateId);
        }

        [Fact]
        public void EventType_ShouldNotBeChanged_AfterObjectInitialization()
        {
            _snapshot.EventType.Should().Be(_eventType);
        }

        [Fact]
        public void SerializedData_ShouldNotBeChanged_AfterObjectInitialization()
        {
            _snapshot.SerializedData.Should().Be(_serializedData);
        }

        [Fact]
        public void Timestamp_ShouldNotBeChanged_AfterObjectInitialization()
        {
            _snapshot.Timestamp.Should().Be(_timestamp);
        }
    }
}