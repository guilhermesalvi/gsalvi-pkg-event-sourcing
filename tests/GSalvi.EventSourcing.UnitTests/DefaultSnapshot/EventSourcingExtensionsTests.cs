using System;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace GSalvi.EventSourcing.UnitTests.DefaultSnapshot
{
    public class EventSourcingExtensionsTests
    {
        private readonly IServiceCollection _services;
        private readonly Mock<Action<IEventSourcingBuilder>> _setupAction;

        public EventSourcingExtensionsTests()
        {
            _services = new ServiceCollection();
            _setupAction = new Mock<Action<IEventSourcingBuilder>>();
        }

        [Fact]
        public void AddEventSourcing_ShouldThrowsAnException_WhenServicesIsNull()
        {
            // Act - Assert
            Assert.Throws<ArgumentNullException>(() =>
                EventSourcingExtensions.AddEventSourcing(default, _setupAction.Object));
        }

        [Fact]
        public void AddEventSourcing_ShouldThrowsAnException_WhenSetupActionIsNull()
        {
            // Act - Assert
            Assert.Throws<ArgumentNullException>(() =>
                _services.AddEventSourcing(default));
        }

        [Fact]
        public void AddEventSourcing_ShouldThrowsAnException_WhenHasNotARepository()
        {
            // Act - Assert
            Assert.Throws<Exception>(() =>
                _services.AddEventSourcing(_setupAction.Object));
        }

        [Fact]
        public void AddEventSourcing_ShouldReturnServices()
        {
            // Arrange
            var repository = new Mock<ISnapshotRepository<Snapshot>>();
            _services.AddScoped(_ => repository.Object);

            // Act - Assert
            _services.AddEventSourcing(_setupAction.Object).Should().BeEquivalentTo(_services);
        }
    }
}