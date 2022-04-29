using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using FluentAssertions.Extensions;
using GSalvi.EventSourcing.Tests.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace GSalvi.EventSourcing.Tests.DefaultEventDataImplementation;

[ExcludeFromCodeCoverage]
public class EventStoreManagerTests : IClassFixture<TestHost<TestStartup>>
{
    private readonly IEventStoreManager _manager;
    private readonly IEventDataRepository<EventData> _repository;

    public EventStoreManagerTests(TestHost<TestStartup> host)
    {
        _manager = host.ServiceProvider.GetRequiredService<IEventStoreManager>();
        _repository = host.ServiceProvider.GetRequiredService<IEventDataRepository<EventData>>();
    }

    [Fact]
    public async Task StoreAsyncMethod_ShouldStoreItemInDatabase()
    {
        // Arrange
        var entity = new Fixture().Create<CustomerRegistered>();

        // Act
        await _manager.StoreAsync(entity, entity.Id);

        // Assert
        var stored = (await _repository.GetByAggregateIdAsync(entity.Id)).FirstOrDefault();
        stored.Should().NotBeNull();
        stored!.Id.Should().NotBe(Guid.Empty);
        stored.AggregateId.Should().Be(entity.Id);
        stored.EventType.Should().Be(nameof(CustomerRegistered));
        stored.Timestamp.Should().BeWithin(2.Seconds()).Before(DateTime.UtcNow);

        var storedEntity = stored.ParsedEntity<CustomerRegistered>();
        storedEntity.Should().NotBeNull();
        storedEntity!.Id.Should().Be(entity.Id);
        storedEntity.Name.Should().Be(entity.Name);
    }
}