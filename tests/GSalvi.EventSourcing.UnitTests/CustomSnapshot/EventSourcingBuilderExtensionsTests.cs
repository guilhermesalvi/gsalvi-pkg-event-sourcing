using System.Linq;
using FluentAssertions;
using GSalvi.EventSourcing.UnitTests.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace GSalvi.EventSourcing.UnitTests.CustomSnapshot
{
    public class EventSourcingBuilderExtensionsTests
    {
        private readonly IServiceCollection _services;
        private readonly IEventSourcingBuilder _builder;

        public EventSourcingBuilderExtensionsTests()
        {
            _services = new ServiceCollection();
            _builder = new EventSourcingBuilder(_services);
        }

        [Fact]
        public void WithSerializer_ShouldAddSerializerAsScoped()
        {
            // Act
            _builder.WithSerializer<MyEventSerializer>();

            // Assert
            var descriptor = _services.SingleOrDefault(x =>
                x.ServiceType == typeof(IEventSerializer));
            descriptor.Should().NotBeNull();
            descriptor?.ImplementationType.Should().Be(typeof(MyEventSerializer));
            descriptor?.Lifetime.Should().Be(ServiceLifetime.Scoped);
        }

        [Fact]
        public void WithSnapshotBuilder_ShouldAddBuilderAsScoped()
        {
            // Act
            _builder.WithSnapshotBuilder<MySnapshotBuilder, MySnapshot>();

            // Assert
            var descriptor = _services.SingleOrDefault(x =>
                x.ServiceType == typeof(ISnapshotBuilder<MySnapshot>));
            descriptor.Should().NotBeNull();
            descriptor?.ImplementationType.Should().Be(typeof(MySnapshotBuilder));
            descriptor?.Lifetime.Should().Be(ServiceLifetime.Scoped);
        }
    }
}