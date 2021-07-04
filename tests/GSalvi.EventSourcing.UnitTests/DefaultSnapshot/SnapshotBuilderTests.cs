using System;
using AutoFixture;
using FluentAssertions;
using Xunit;

namespace GSalvi.EventSourcing.UnitTests.DefaultSnapshot
{
    public class SnapshotBuilderTests
    {
        private readonly Guid _aggregateId;
        private readonly string _eventType;
        private readonly string _serializedData;
        private readonly SnapshotBuilder _builder;

        public SnapshotBuilderTests()
        {
            var fixture = new Fixture();
            _aggregateId = fixture.Create<Guid>();
            _eventType = fixture.Create<string>();
            _serializedData = fixture.Create<string>();
            _builder = new SnapshotBuilder();
        }

        [Fact]
        public void Create_ShouldSetCorrectProperty()
        {
            // Act
            var result = _builder.Create(
                _aggregateId,
                _eventType,
                _serializedData);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().NotBe(Guid.Empty);
            result.AggregateId.Should().Be(_aggregateId);
            result.EventType.Should().Be(_eventType);
            result.SerializedData.Should().Be(_serializedData);
            result.Timestamp.Should().NotBe(default);
        }
    }
}