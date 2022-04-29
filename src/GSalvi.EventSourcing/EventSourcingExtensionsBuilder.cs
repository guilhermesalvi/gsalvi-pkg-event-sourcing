using Microsoft.Extensions.DependencyInjection;

namespace GSalvi.EventSourcing;

/// <summary>
/// Defines a builder to registering dependencies
/// </summary>
/// <typeparam name="TEventData"></typeparam>
public class EventSourcingExtensionsBuilder<TEventData>
    where TEventData : EventData
{
    /// <summary>
    /// Represents a collection of services
    /// </summary>
    public IServiceCollection Services { get; }

    internal EventSourcingExtensionsBuilder(IServiceCollection services) =>
        Services = services ?? throw new ArgumentNullException(nameof(services));

    /// <summary>
    /// Adds a new builder for <typeparamref name="TEventData"/> type
    /// </summary>
    /// <typeparam name="TEventDataBuilder"></typeparam>
    public void WithEventDataBuilder<TEventDataBuilder>()
        where TEventDataBuilder : IEventDataBuilder<TEventData>
    {
        Services.AddScoped(typeof(IEventDataBuilder<TEventData>), typeof(TEventDataBuilder));
    }
}