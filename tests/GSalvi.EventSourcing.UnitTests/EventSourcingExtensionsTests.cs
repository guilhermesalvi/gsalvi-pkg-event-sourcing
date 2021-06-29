using System;
using System.Linq;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace GSalvi.EventSourcing.UnitTests
{
    public class EventSourcingExtensionsTests
    {
        private readonly IServiceCollection _services;
        private readonly IMock<Action<IEventSourcingBuilder>> _setupAction;

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
                EventSourcingExtensions.AddEventSourcing(null, _setupAction.Object));
        }

        [Fact]
        public void AddEventSourcing_ShouldThrowsAnException_WhenSetupActionIsNull()
        {
            // Act - Assert
            Assert.Throws<ArgumentNullException>(() => _services.AddEventSourcing(null));
        }

        [Fact]
        public void ShouldAddDefaultSnapshotBuilder_WhenItIsNotInformed()
        {
            // Act
            _services.AddEventSourcing(_ => { });

            // Assert
            var descriptor = _services.SingleOrDefault(x =>
                x.ServiceType == typeof(ISnapshotBuilder<Snapshot>));
            descriptor.Should().NotBeNull();
        }
    }
}