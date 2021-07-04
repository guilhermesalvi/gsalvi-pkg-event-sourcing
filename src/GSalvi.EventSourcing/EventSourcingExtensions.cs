using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace GSalvi.EventSourcing
{
    /// <summary>
    /// Defines extension methods to registering dependencies.
    /// </summary>
    public static class EventSourcingExtensions
    {
        /// <summary>
        /// Add required services to ASP.NET container.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="setupAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddEventSourcing(
            this IServiceCollection services,
            Action<IEventSourcingBuilder> setupAction)
        {
            if (services is null) throw new ArgumentNullException(nameof(services));
            if (setupAction is null) throw new ArgumentNullException(nameof(setupAction));

            var builder = (IEventSourcingBuilder) new EventSourcingBuilder(services);
            setupAction.Invoke(builder);

            services.AddScoped<IEventSerializer, EventSerializer>();
            services.AddScoped<IEventStoreManager, EventStoreManager>();
            services.AddScoped<ISnapshotBuilder, SnapshotBuilder>();

            if (services.SingleOrDefault(x =>
                x.ServiceType == typeof(ISnapshotRepository<Snapshot>)) is null)
            {
                throw new Exception(
                    $"An implementation for {nameof(ISnapshotRepository<Snapshot>)} has not been defined. Consider adding a database.");
            }

            return services;
        }

        /// <summary>
        /// Add required services to ASP.NET container with
        /// <see cref="Snapshot"/> representing by <typeparamref name="T"/>.
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

            services.AddScoped<IEventSerializer, EventSerializer>();
            services.AddScoped<IEventStoreManager<T>, EventStoreManager<T>>();

            if (services.SingleOrDefault(x =>
                x.ServiceType == typeof(ISnapshotRepository<T>)) is null)
            {
                throw new Exception(
                    $"An implementation for {nameof(ISnapshotRepository<T>)} has not been defined. Consider adding a database.");
            }

            if (services.SingleOrDefault(x =>
                x.ServiceType == typeof(ISnapshotBuilder<T>)) is null)
            {
                throw new Exception(
                    $"An implementation for {nameof(ISnapshotBuilder<T>)} has not been defined.");
            }

            return services;
        }
    }
}