using System;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace GSalvi.EventSourcing.UnitTests.DefaultSnapshot
{
    public class EventSourcingBuilderTests
    {
        private readonly ServiceCollection _services;
        private readonly EventSourcingBuilder _builder;

        public EventSourcingBuilderTests()
        {
            _services = new ServiceCollection();
            _builder = new EventSourcingBuilder(_services);
        }

        [Fact]
        public void Constructor_ShouldThrowsAnExceptionWhenServicesIsNull()
        {
            // Act - Assert
            Assert.Throws<ArgumentNullException>(() => new EventSourcingBuilder(default));
        }

        [Fact]
        public void Constructor_ShouldSetServicesProperty()
        {
            // Assert
            _builder.Services.Should().BeEquivalentTo(_services);
        }
    }
}