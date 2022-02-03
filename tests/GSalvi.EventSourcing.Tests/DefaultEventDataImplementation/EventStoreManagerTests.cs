using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using GSalvi.EventSourcing.Tests.Configurations;
using GSalvi.EventSourcing.Tests.Configurations.Fakers;
using GSalvi.EventSourcing.Tests.Configurations.Models;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace GSalvi.EventSourcing.Tests.DefaultEventDataImplementation;

[Collection(nameof(TestsFixtureCollection))]
public class EventStoreManagerTests
{
    private readonly FakeDefaultEventDataRepository _repository;
    private readonly IEventStoreManager _manager;
    private readonly Fixture _fixture;

    public EventStoreManagerTests(TestHost<DefaultEventDataTestStartup> testHost)
    {
        _repository = (FakeDefaultEventDataRepository) testHost.ServiceProvider
            .GetRequiredService<IEventDataRepository<EventData>>();
        _manager = testHost.ServiceProvider.GetRequiredService<IEventStoreManager>();
        _fixture = new Fixture();
    }

    [Fact]
    public async Task StoreAsync_ShouldStoreOneItemInRepository()
    {
        // Arrange
        var aggregateId = _fixture.Create<Guid>();
        const string eventType = nameof(CustomerRegistered);
        var customerRegistered = _fixture.Create<CustomerRegistered>();

        // Act
        await _manager
            .StoreAsync(customerRegistered, aggregateId, eventType)
            .ConfigureAwait(false);

        // Assert
        var fromRepository = await _repository.GetByAggregateIdAsync(aggregateId);
        var data = fromRepository.FirstOrDefault();
        data.Should().NotBeNull();
        data!.Id.Should().NotBe(Guid.Empty);
        data.AggregateId.Should().Be(aggregateId);
        data.EventType.Should().Be(eventType);
        data.Timestamp.Should().NotBe(default);
        var parsedCustomerRegistered = (CustomerRegistered) data.Entity!;
        parsedCustomerRegistered.CustomerId.Should().Be(customerRegistered.CustomerId);
        parsedCustomerRegistered.CustomerName.Should().Be(customerRegistered.CustomerName);
    }
}