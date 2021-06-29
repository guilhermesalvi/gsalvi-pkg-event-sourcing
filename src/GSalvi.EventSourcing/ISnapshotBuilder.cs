using System;

namespace GSalvi.EventSourcing
{
    /// <summary>
    /// Defines a builder of T type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISnapshotBuilder<out T> where T : Snapshot
    {
        /// <summary>
        /// Returns a new T object.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="aggregateId"></param>
        /// <param name="eventType"></param>
        /// <param name="serializedData"></param>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        T Create(
            Guid id,
            Guid aggregateId,
            string eventType,
            string serializedData,
            DateTime timestamp);
    }
}