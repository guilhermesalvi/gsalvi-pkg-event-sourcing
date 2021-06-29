using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace GSalvi.EventSourcing
{
    /// <summary>
    /// Defines extension methods to registering dependencies.
    /// </summary>
    public static class EventSourcingExtensions
    {
        /// <summary>
        /// Adds required services to ASP.NET container.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="setupAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddEventSourcing(
            this IServiceCollection services,
            Action<IEventSourcingBuilder> setupAction)
        {
            return services.AddEventSourcing<Snapshot>(setupAction);
        }

        /// <summary>
        /// Adds required services to ASP.NET container with
        /// <see cref="Snapshot"/> representing by <see cref="T"/>.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="setupAction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IServiceCollection AddEventSourcing<T>(
            this IServiceCollection services,
            Action<IEventSourcingBuilder> setupAction)
            where T : Snapshot
        {
            if (services is null) throw new ArgumentNullException(nameof(services));
            if (setupAction is null) throw new ArgumentNullException(nameof(setupAction));

            var builder = (IEventSourcingBuilder) new EventSourcingBuilder(services);
            setupAction.Invoke(builder);

            services.TryAddScoped<IEventSerializer, EventSerializer>();
            services.AddScoped<IEventStoreManager<T>, EventStoreManager<T>>();

            if (services.SingleOrDefault(x =>
                x.ServiceType == typeof(ISnapshotBuilder<T>)) is null)
            {
                services.AddScoped<ISnapshotBuilder<Snapshot>, SnapshotBuilder>();
            }

            return services;
        }
    }
}