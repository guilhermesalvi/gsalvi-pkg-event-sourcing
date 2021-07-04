using System;

namespace GSalvi.EventSourcing
{
    /// <summary>
    /// Defines a builder of <see cref="Snapshot"/>.
    /// </summary>
    public interface ISnapshotBuilder : ISnapshotBuilder<Snapshot>
    {
    }

    /// <summary>
    /// Defines a builder of <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISnapshotBuilder<out T> where T : Snapshot
    {
        /// <summary>
        /// Returns a new <typeparamref name="T"/>.
        /// </summary>
        /// <param name="aggregateId"></param>
        /// <param name="eventType"></param>
        /// <param name="serializedData"></param>
        /// <returns></returns>
        T Create(
            Guid aggregateId,
            string eventType,
            string serializedData);
    }
}