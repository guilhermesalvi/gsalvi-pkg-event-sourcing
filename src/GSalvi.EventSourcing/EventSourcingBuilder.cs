using System;
using Microsoft.Extensions.DependencyInjection;

namespace GSalvi.EventSourcing
{
    internal class EventSourcingBuilder : IEventSourcingBuilder
    {
        public IServiceCollection Services { get; }

        public EventSourcingBuilder(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }
    }
}