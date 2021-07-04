using System;

namespace GSalvi.EventSourcing
{
    /// <summary>
    /// Defines a snapshot class.
    /// </summary>
    public class Snapshot
    {
        /// <summary>
        /// Represents an identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Represents an aggregate id.
        /// </summary>
        public Guid AggregateId { get; set; }

        /// <summary>
        /// Represents an event type.
        /// </summary>
        public string EventType { get; set; }

        /// <summary>
        /// Represents a serialized event data.
        /// </summary>
        public string SerializedData { get; set; }

        /// <summary>
        /// Represents a timestamp.
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}