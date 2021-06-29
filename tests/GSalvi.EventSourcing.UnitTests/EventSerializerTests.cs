using System.Text.Json;
using AutoFixture;
using FluentAssertions;
using GSalvi.EventSourcing.UnitTests.Configurations;
using Xunit;

namespace GSalvi.EventSourcing.UnitTests
{
    public class EventSerializerTests
    {
        private readonly string _serialized;
        private readonly DummyEvent _dummyEvent;
        private readonly EventSerializer _serializer;

        public EventSerializerTests()
        {
            _dummyEvent = new Fixture().Create<DummyEvent>();
            _serialized = JsonSerializer.Serialize(_dummyEvent);
            _serializer = new EventSerializer();
        }

        [Fact]
        public void Serialize_ShouldSerializeAnObject()
        {
            _serializer.Serialize(_dummyEvent).Should().Be(_serialized);
        }
    }
}