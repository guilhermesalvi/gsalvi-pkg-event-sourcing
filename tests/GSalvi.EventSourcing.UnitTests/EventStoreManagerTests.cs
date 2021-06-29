using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using GSalvi.EventSourcing.UnitTests.Configurations;
using Moq;
using Xunit;

namespace GSalvi.EventSourcing.UnitTests
{
    public class EventStoreManagerTests
    {
        private readonly Mock<ISnapshotRepository<DummySnapshot>> _repository;
        private readonly EventStoreManager<DummySnapshot> _manager;
        private readonly DummySnapshot _dummySnapshot;
        private readonly Guid _aggregateId;
        private readonly DummyEvent _dummyEvent;
        private readonly Fixture _fixture;

        public EventStoreManagerTests()
        {
            _repository = new Mock<ISnapshotRepository<DummySnapshot>>();
            var builder = new Mock<ISnapshotBuilder<DummySnapshot>>();
            var serializer = new EventSerializer();
            _manager = new EventStoreManager<DummySnapshot>(_repository.Object, builder.Object, serializer);

            _fixture = new Fixture();
            _dummySnapshot = new DummySnapshot(
                _fixture.Create<Guid>(),
                _fixture.Create<Guid>(),
                _fixture.Create<string>(),
                _fixture.Create<string>(),
                _fixture.Create<DateTime>());
            _aggregateId = _dummySnapshot.AggregateId;
            builder.Setup(x =>
                    x.Create(
                        It.IsAny<Guid>(),
                        It.Is<Guid>(m =>
                            m == _aggregateId),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<DateTime>()))
                .Returns(_dummySnapshot);
            _dummyEvent = _fixture.Create<DummyEvent>();
        }

        [Fact]
        public async Task StoreAsync_ShouldCallsRepositoryWithCorrectProps()
        {
            // Act
            await _manager.StoreAsync(_dummyEvent, _aggregateId);

            // Assert
            _repository.Verify(x =>
                x.AddAsync(It.Is<DummySnapshot>(m =>
                    m.Id == _dummySnapshot.Id &&
                    m.AggregateId == _dummySnapshot.AggregateId &&
                    m.EventType == _dummySnapshot.EventType &&
                    m.SerializedData == _dummySnapshot.SerializedData &&
                    m.Timestamp == _dummySnapshot.Timestamp)), Times.Exactly(1));
        }

        [Fact]
        public async Task GetByIdAsync_ShouldCallsRepositoryWithCorrectProps()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            _repository.Setup(x =>
                x.GetByIdAsync(It.Is<Guid>(m =>
                    m == id))).ReturnsAsync(_dummySnapshot);

            // Act
            var result = await _manager.GetByIdAsync(id);

            // Assert
            result.Should().NotBeNull();
            result.Should().Be(_dummySnapshot);
            _repository.Verify(x =>
                x.GetByIdAsync(It.Is<Guid>(m =>
                    m == id)), Times.Exactly(1));
        }

        [Fact]
        public async Task GetByAggregateIdAsync_ShouldCallsRepositoryWithCorrectProps()
        {
            // Arrange
            var aggregateId = _fixture.Create<Guid>();
            var dummySnapshotCollection = _fixture.CreateMany<DummySnapshot>(1).ToList();
            _repository.Setup(x =>
                x.GetByAggregateIdAsync(It.Is<Guid>(m =>
                    m == aggregateId))).ReturnsAsync(dummySnapshotCollection);

            // Act
            var result = await _manager.GetByAggregateIdAsync(aggregateId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(dummySnapshotCollection);
            _repository.Verify(x =>
                x.GetByAggregateIdAsync(It.Is<Guid>(m =>
                    m == aggregateId)), Times.Exactly(1));
        }

        [Fact]
        public async Task GetByEventTypeAsync_ShouldCallsRepositoryWithCorrectProps()
        {
            // Arrange
            var eventType = _fixture.Create<string>();
            var dummySnapshotCollection = _fixture.CreateMany<DummySnapshot>(1).ToList();
            _repository.Setup(x =>
                x.GetByEventTypeAsync(It.Is<string>(m =>
                    m == eventType))).ReturnsAsync(dummySnapshotCollection);

            // Act
            var result = await _manager.GetByEventTypeAsync(eventType);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(dummySnapshotCollection);
            _repository.Verify(x =>
                x.GetByEventTypeAsync(It.Is<string>(m =>
                    m == eventType)), Times.Exactly(1));
        }
    }
}