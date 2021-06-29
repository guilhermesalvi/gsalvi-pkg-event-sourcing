using Microsoft.Extensions.DependencyInjection;

namespace GSalvi.EventSourcing
{
    /// <summary>
    /// Defines a builder to register dependencies
    /// of <see cref="EventSourcing"/> package.
    /// </summary>
    public interface IEventSourcingBuilder
    {
        /// <summary>
        /// Represents a collection of container services.
        /// </summary>
        IServiceCollection Services { get; }
    }
}