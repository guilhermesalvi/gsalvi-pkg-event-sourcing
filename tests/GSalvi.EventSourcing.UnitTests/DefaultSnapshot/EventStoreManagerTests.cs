using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using GSalvi.EventSourcing.UnitTests.Configurations;
using Moq;
using Xunit;

namespace GSalvi.EventSourcing.UnitTests.DefaultSnapshot
{
    public class EventStoreManagerTests
    {
        private readonly Mock<ISnapshotRepository> _repository;
        private readonly Mock<ISnapshotBuilder> _builder;
        private readonly Mock<IEventSerializer> _serializer;
        private readonly IEventStoreManager _manager;
        private readonly Fixture _fixture;

        public EventStoreManagerTests()
        {
            _repository = new Mock<ISnapshotRepository>();
            _builder = new Mock<ISnapshotBuilder>();
            _serializer = new Mock<IEventSerializer>();
            _manager = new EventStoreManager(_repository.Object, _builder.Object, _serializer.Object);

            _fixture = new Fixture();
        }

        [Fact]
        public async Task StoreAsync_ShouldCallRepository()
        {
            // Arrange
            var aggregateId = _fixture.Create<Guid>();
            var myDummyEvent = _fixture.Create<MyDummyEvent>();
            var snapshot = _fixture.Create<Snapshot>();
            var serialized = _fixture.Create<string>();
            _serializer.Setup(x =>
                x.Serialize(It.Is<object>(m =>
                    m == myDummyEvent))).Returns(serialized);
            _builder.Setup(x =>
                x.Create(It.Is<Guid>(m =>
                    m == aggregateId), It.Is<string>(m =>
                    m == nameof(MyDummyEvent)), It.Is<string>(m =>
                    m == serialized))).Returns(snapshot);

            // Act
            await _manager.StoreAsync(myDummyEvent, aggregateId);

            // Assert
            _repository.Verify(x =>
                x.AddAsync(It.Is<Snapshot>(m =>
                    m == snapshot)), Times.Exactly(1));
        }

        [Fact]
        public async Task GetByIdAsync_ShouldCallRepository()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            var snapshot = _fixture.Create<Snapshot>();
            _repository.Setup(x =>
                x.GetByIdAsync(It.Is<Guid>(m =>
                    m == id))).ReturnsAsync(snapshot);

            // Act
            var result = await _manager.GetByIdAsync(id);

            // Assert
            result.Should().NotBeNull();
            result.Should().Be(snapshot);
            _repository.Verify(x =>
                x.GetByIdAsync(It.Is<Guid>(m =>
                    m == id)), Times.Exactly(1));
        }

        [Fact]
        public async Task GetByAggregateIdAsync_ShouldCallRepository()
        {
            // Arrange
            var aggregateId = _fixture.Create<Guid>();
            var snapshots = _fixture.CreateMany<Snapshot>();
            _repository.Setup(x =>
                x.GetByAggregateIdAsync(It.Is<Guid>(m =>
                    m == aggregateId))).ReturnsAsync(snapshots);

            // Act
            var result = await _manager.GetByAggregateIdAsync(aggregateId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(snapshots);
            _repository.Verify(x =>
                x.GetByAggregateIdAsync(It.Is<Guid>(m =>
                    m == aggregateId)), Times.Exactly(1));
        }

        [Fact]
        public async Task GetByEventTypeAsync_ShouldCallRepository()
        {
            // Arrange
            var eventType = _fixture.Create<string>();
            var snapshots = _fixture.CreateMany<Snapshot>();
            _repository.Setup(x =>
                x.GetByEventTypeAsync(It.Is<string>(m =>
                    m == eventType))).ReturnsAsync(snapshots);

            // Act
            var result = await _manager.GetByEventTypeAsync(eventType);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(snapshots);
            _repository.Verify(x =>
                x.GetByEventTypeAsync(It.Is<string>(m =>
                    m == eventType)), Times.Exactly(1));
        }
    }
}