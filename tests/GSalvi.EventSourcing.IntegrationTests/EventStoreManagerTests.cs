using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using GSalvi.EventSourcing.IntegrationTests.Configurations;
using GSalvi.EventSourcing.IntegrationTests.Fakers;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace GSalvi.EventSourcing.IntegrationTests
{
    [Collection(nameof(IntegrationTestsFixtureCollection))]
    public class EventStoreManagerTests
    {
        private readonly IEventStoreManager<MySnapshot> _manager;
        private readonly FakeSnapshotRepository _repository;

        public EventStoreManagerTests(IntegrationTestsFixture<TestStartup> testsFixture)
        {
            _manager = testsFixture.TestHost.ServiceProvider.GetRequiredService<IEventStoreManager<MySnapshot>>();
            _repository = (FakeSnapshotRepository) testsFixture.TestHost.ServiceProvider
                .GetRequiredService<ISnapshotRepository<MySnapshot>>();
        }

        [Fact]
        public async Task StoreAsync_ShouldStoreOneItemInRepository()
        {
            // Arrange
            var aggregateId = new Fixture().Create<Guid>();
            var dummyEvent = new MyDummyEvent();

            // Act
            await _manager.StoreAsync(dummyEvent, aggregateId);

            // Assert
            _repository.Data.FirstOrDefault(x => x.AggregateId == aggregateId).Should().NotBeNull();
        }

        [Fact]
        public async Task GetByIdAsync_ShouldGetOneItemFromRepository()
        {
            // Arrange
            var snapshot = _repository.Data.FirstOrDefault();

            // Act
            var result = await _manager.GetByIdAsync(snapshot!.Id);

            // Assert
            result.Should().NotBeNull();
            result.Should().Be(snapshot);
        }

        [Fact]
        public async Task GetByAggregateIdAsync_ShouldGetOneItemFromRepository()
        {
            // Arrange
            var aggregateId = new Guid("3c8e2dc0-68bd-4a1f-baa9-bbe675d4a63b");
            var snapshots = _repository.Data.Where(x => x.AggregateId == aggregateId);

            // Act
            var result = await _manager.GetByAggregateIdAsync(aggregateId);

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().Be(2);
            result.Should().BeEquivalentTo(snapshots);
        }
        
        [Fact]
        public async Task GetByEventTypeAsync_ShouldGetOneItemFromRepository()
        {
            // Arrange
            var eventType = "MyDummyEvent";
            var snapshots = _repository.Data.Where(x => x.EventType == eventType);

            // Act
            var result = await _manager.GetByEventTypeAsync(eventType);

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().Be(2);
            result.Should().BeEquivalentTo(snapshots);
        }
    }
}