using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace GSalvi.EventSourcing;

[ExcludeFromCodeCoverage]
public static class EventSourcingExtensions
{
    public static IServiceCollection AddEventSourcing(
        this IServiceCollection services,
        Action<IEventSourcingExtensionsBuilder> setupAction)
    {
        if (services is null) throw new ArgumentNullException(nameof(services));
        if (setupAction is null) throw new ArgumentNullException(nameof(setupAction));

        var builder = new EventSourcingExtensionsBuilder(services);
        setupAction.Invoke(builder);

        services.AddScoped<IEventStoreManager, EventStoreManager>();
        services.AddScoped<IEventDataBuilder, EventDataBuilder>();

        if (services.SingleOrDefault(x => x.ServiceType == typeof(IEventDataRepository<EventData>)) is null)
            throw new ArgumentException(
                $"An implementation for {nameof(IEventDataRepository<EventData>)} has not been defined. Consider adding a database");

        return services;
    }

    public static IServiceCollection AddEventSourcing<TEventData>(
        this IServiceCollection services,
        Action<IEventSourcingExtensionsBuilder> setupAction)
        where TEventData : EventData
    {
        if (services is null) throw new ArgumentNullException(nameof(services));
        if (setupAction is null) throw new ArgumentNullException(nameof(setupAction));

        var builder = new EventSourcingExtensionsBuilder(services);
        setupAction.Invoke(builder);

        services.AddScoped<IEventStoreManager, EventStoreManager<TEventData>>();

        if (services.SingleOrDefault(x => x.ServiceType == typeof(IEventDataRepository<TEventData>)) is null)
            throw new ArgumentException(
                $"An implementation for {nameof(IEventDataRepository<TEventData>)} has not been defined. Consider adding a database");

        if (services.SingleOrDefault(x =>
                x.ServiceType == typeof(IEventDataBuilder<TEventData>)) is null)
            throw new ArgumentException(
                $"An implementation for {nameof(IEventDataBuilder<TEventData>)} has not been defined. Consider adding a custom builder");

        return services;
    }
}