using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace GSalvi.EventSourcing;

/// <summary>
/// Defines extension methods to registering dependencies and middlewares
/// </summary>
[ExcludeFromCodeCoverage]
public static class EventSourcingExtensions
{
    /// <summary>
    /// Adds required services to ASP.NET container
    /// </summary>
    /// <param name="services"></param>
    /// <param name="setupAction"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public static IServiceCollection AddEventSourcing(
        this IServiceCollection services,
        Action<EventSourcingExtensionsBuilder<EventData>> setupAction)
    {
        if (services is null) throw new ArgumentNullException(nameof(services));
        if (setupAction is null) throw new ArgumentNullException(nameof(setupAction));

        var builder = new EventSourcingExtensionsBuilder<EventData>(services);
        setupAction.Invoke(builder);

        services.AddScoped<IEventStoreManager, EventStoreManager>();
        services.AddScoped<IEventDataBuilder<EventData>, EventDataBuilder>();

        if (services.SingleOrDefault(x => x.ServiceType == typeof(IEventDataRepository<EventData>)) is null)
            throw new ArgumentException(
                $"An implementation for {nameof(IEventDataRepository<EventData>)} has not been defined. Consider adding a database");

        return services;
    }

    /// <summary>
    /// Adds required services to ASP.NET container
    /// </summary>
    /// <param name="services"></param>
    /// <param name="setupAction"></param>
    /// <typeparam name="TEventData"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public static IServiceCollection AddEventSourcing<TEventData>(
        this IServiceCollection services,
        Action<EventSourcingExtensionsBuilder<TEventData>> setupAction)
        where TEventData : EventData
    {
        if (services is null) throw new ArgumentNullException(nameof(services));
        if (setupAction is null) throw new ArgumentNullException(nameof(setupAction));

        var builder = new EventSourcingExtensionsBuilder<TEventData>(services);
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