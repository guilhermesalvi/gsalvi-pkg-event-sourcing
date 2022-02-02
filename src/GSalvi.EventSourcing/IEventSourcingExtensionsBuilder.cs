using Microsoft.Extensions.DependencyInjection;

namespace GSalvi.EventSourcing;

public interface IEventSourcingExtensionsBuilder
{
    IServiceCollection Services { get; }
}