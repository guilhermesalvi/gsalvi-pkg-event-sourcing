using System.Text.Json;
using AutoFixture;
using FluentAssertions;
using GSalvi.EventSourcing.UnitTests.Configurations;
using Xunit;

namespace GSalvi.EventSourcing.UnitTests.DefaultSnapshot
{
    public class EventSerializerTests
    {
        private readonly EventSerializer _serializer;
        private readonly MyDummyEvent _event;
        private readonly string _serialized;

        public EventSerializerTests()
        {
            _serializer = new EventSerializer();
            _event = new Fixture().Create<MyDummyEvent>();
            _serialized = JsonSerializer.Serialize(_event);
        }

        [Fact]
        public void Serialize_ShouldSerializeEventWithSystemTextJson()
        {
            // Act
            var result = _serializer.Serialize(_event);

            // Assert
            result.Should().NotBeNull();
            result.Should().Be(_serialized);
        }
    }
}