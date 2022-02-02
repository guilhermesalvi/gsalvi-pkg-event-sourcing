using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace GSalvi.EventSourcing;

[ExcludeFromCodeCoverage]
public static class EventDataBuilderExtensions
{
    public static IEventSourcingExtensionsBuilder WithEventDataBuilder
        <TEventData, TEventDataBuilder>(
            this IEventSourcingExtensionsBuilder builder)
        where TEventData : EventData
        where TEventDataBuilder : IEventDataBuilder<TEventData>
    {
        builder.Services.AddScoped(typeof(IEventDataBuilder<TEventData>), typeof(TEventDataBuilder));
        return builder;
    }
}