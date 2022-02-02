using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace GSalvi.EventSourcing;

[ExcludeFromCodeCoverage]
internal class EventSourcingExtensionsBuilder : IEventSourcingExtensionsBuilder
{
    public IServiceCollection Services { get; }

    public EventSourcingExtensionsBuilder(IServiceCollection services) =>
        Services = services ?? throw new ArgumentNullException(nameof(services));
}