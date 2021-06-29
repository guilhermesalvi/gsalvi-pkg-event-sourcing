using System;
using AutoFixture;
using FluentAssertions;
using Xunit;

namespace GSalvi.EventSourcing.UnitTests
{
    public class SnapshotBuilderTests
    {
        private readonly Guid _id;
        private readonly Guid _aggregateId;
        private readonly string _eventType;
        private readonly string _serializedData;
        private readonly DateTime _timestamp;
        private readonly SnapshotBuilder _builder;

        public SnapshotBuilderTests()
        {
            var fixture = new Fixture();
            _id = fixture.Create<Guid>();
            _aggregateId = fixture.Create<Guid>();
            _eventType = fixture.Create<string>();
            _serializedData = fixture.Create<string>();
            _timestamp = fixture.Create<DateTime>();
            _builder = new SnapshotBuilder();
        }

        [Fact]
        public void Create_ShouldSetCorrectProperties()
        {
            // Act
            var result = _builder.Create(_id, _aggregateId, _eventType, _serializedData, _timestamp);

            // Assert
            result.Id.Should().Be(_id);
            result.AggregateId.Should().Be(_aggregateId);
            result.EventType.Should().Be(_eventType);
            result.SerializedData.Should().Be(_serializedData);
            result.Timestamp.Should().Be(_timestamp);
        }
    }
}