using System;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace GSalvi.EventSourcing.UnitTests
{
    public class EventSourcingBuilderTests
    {
        [Fact]
        public void Constructor_ShouldThrowsAnException_WhenServicesIsNull()
        {
            // Act - Assert
            Assert.Throws<ArgumentNullException>(() => new EventSourcingBuilder(null));
        }

        [Fact]
        public void Constructor_ShouldSetServicesProperty()
        {
            // Arrange - Act
            var services = new Mock<IServiceCollection>();
            var builder = new EventSourcingBuilder(services.Object);

            // Assert
            builder.Services.Should().NotBeNull();
            builder.Services.Should().BeEquivalentTo(services.Object);
        }
    }
}