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

namespace GSalvi.EventSourcing.Tests.CustomEventDataImplementation;

[Collection(nameof(TestsFixtureCollection))]
public class EventStoreManagerTests
{
    private readonly FakeCustomEventDataRepository _repository;
    private readonly IEventStoreManager _manager;
    private readonly Fixture _fixture;

    public EventStoreManagerTests(TestHost<CustomEventDataTestStartup> testHost)
    {
        _repository = (FakeCustomEventDataRepository) testHost.ServiceProvider
            .GetRequiredService<IEventDataRepository<CustomEventData>>();
        _manager = testHost.ServiceProvider.GetRequiredService<IEventStoreManager>();
        _fixture = new Fixture();
    }

    [Fact]
    public async Task StoreAsync_ShouldStoreOneItemInRepository()
    {
        // Arrange
        var aggregateId = _fixture.Create<Guid>();
        var customerRegistered = _fixture.Create<CustomerRegistered>();
        var userId = new Guid("e872aa96-2c62-4a99-9bb5-fcdcd0fb093e");
        const string userName = "Jaqueline Moura";
        const string userEmail = "jaqueline.moura@sorocaba.com.br";

        // Act
        await _manager
            .StoreAsync(customerRegistered, aggregateId, nameof(CustomerRegistered))
            .ConfigureAwait(false);

        // Assert
        var fromRepository = await _repository.GetByAggregateIdAsync(aggregateId);
        var data = fromRepository.FirstOrDefault();
        data.Should().NotBeNull();
        data!.Id.Should().NotBe(Guid.Empty);
        data.AggregateId.Should().Be(aggregateId);
        data.EventType.Should().Be(nameof(CustomerRegistered));
        data.Timestamp.Should().NotBe(default);
        data.UserId.Should().Be(userId);
        data.UserName.Should().Be(userName);
        data.UserEmail.Should().Be(userEmail);
        var parsedCustomerRegistered = (CustomerRegistered) data.Entity!;
        parsedCustomerRegistered.CustomerId.Should().Be(customerRegistered.CustomerId);
        parsedCustomerRegistered.CustomerName.Should().Be(customerRegistered.CustomerName);
    }
}