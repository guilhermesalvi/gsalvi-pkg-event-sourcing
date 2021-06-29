using System;

namespace GSalvi.EventSourcing
{
    /// <summary>
    /// Defines a snapshot class.
    /// </summary>
    public class Snapshot
    {
        /// <summary>
        /// Represents the identifier of <see cref="Snapshot"/>.
        /// </summary>
        public Guid Id { get; }
        
        /// <summary>
        /// Represents the aggregate id of <see cref="Snapshot"/>.
        /// </summary>
        public Guid AggregateId { get; }
        
        /// <summary>
        /// Represents the event type of <see cref="Snapshot"/>.
        /// </summary>
        public string EventType { get; }
        
        /// <summary>
        /// Represents the serialized data of <see cref="Snapshot"/>.
        /// </summary>
        public string SerializedData { get; }
        
        /// <summary>
        /// Represents the timestamp of <see cref="Snapshot"/>.
        /// </summary>
        public DateTime Timestamp { get; }

        /// <summary>
        /// Creates a new <see cref="Snapshot"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="aggregateId"></param>
        /// <param name="eventType"></param>
        /// <param name="serializedData"></param>
        /// <param name="timestamp"></param>
        public Snapshot(
            Guid id,
            Guid aggregateId,
            string eventType,
            string serializedData,
            DateTime timestamp)
        {
            Id = id;
            AggregateId = aggregateId;
            EventType = eventType;
            SerializedData = serializedData;
            Timestamp = timestamp;
        }
    }
}