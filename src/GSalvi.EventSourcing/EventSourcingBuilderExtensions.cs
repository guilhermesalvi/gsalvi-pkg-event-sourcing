using Microsoft.Extensions.DependencyInjection;

namespace GSalvi.EventSourcing
{
    /// <summary>
    /// Defines an extension class to <see cref="IEventSourcingBuilder"/>.
    /// </summary>
    public static class EventSourcingBuilderExtensions
    {
        /// <summary>
        /// Adds a new <see cref="T"/> type event serializer.
        /// </summary>
        /// <param name="builder"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEventSourcingBuilder WithSerializer<T>(
            this IEventSourcingBuilder builder)
            where T : IEventSerializer
        {
            builder.Services.AddScoped(typeof(IEventSerializer), typeof(T));
            return builder;
        }

        /// <summary>
        /// Adds a new <see cref="T"/> type snapshot builder.
        /// </summary>
        /// <param name="builder"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TR"></typeparam>
        /// <returns></returns>
        public static IEventSourcingBuilder WithSnapshotBuilder<T, TR>(
            this IEventSourcingBuilder builder)
            where T : ISnapshotBuilder<TR>
            where TR : Snapshot
        {
            builder.Services.AddScoped(typeof(ISnapshotBuilder<TR>), typeof(T));
            return builder;
        }
    }
}