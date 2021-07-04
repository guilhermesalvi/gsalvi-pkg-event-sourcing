using System;
using GSalvi.EventSourcing.UnitTests.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace GSalvi.EventSourcing.UnitTests.CustomSnapshot
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
                EventSourcingExtensions.AddEventSourcing<MySnapshot>(default, _setupAction.Object));
        }

        [Fact]
        public void AddEventSourcing_ShouldThrowsAnException_WhenSetupActionIsNull()
        {
            // Act - Assert
            Assert.Throws<ArgumentNullException>(() =>
                _services.AddEventSourcing<MySnapshot>(default));
        }

        [Fact]
        public void AddEventSourcing_ShouldThrowsAnException_WhenHasNotARepository()
        {
            // Act - Assert
            Assert.Throws<Exception>(() =>
                _services.AddEventSourcing<MySnapshot>(_setupAction.Object));
        }

        [Fact]
        public void AddEventSourcing_ShouldThrowsAnException_WhenHasNotABuilder()
        {
            // Arrange
            var repository = new Mock<ISnapshotRepository<MySnapshot>>();
            _services.AddScoped(_ => repository.Object);

            // Act - Assert
            Assert.Throws<Exception>(() =>
                _services.AddEventSourcing<MySnapshot>(_setupAction.Object));
        }
    }
}