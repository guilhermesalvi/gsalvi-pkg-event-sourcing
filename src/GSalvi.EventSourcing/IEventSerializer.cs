namespace GSalvi.EventSourcing
{
    /// <summary>
    /// Defines a serializer of an event class.
    /// </summary>
    public interface IEventSerializer
    {
        /// <summary>
        /// Returns a new string of serialized <typeparamref name="TR"/>.
        /// </summary>
        /// <param name="event"></param>
        /// <typeparam name="TR"></typeparam>
        /// <returns></returns>
        string Serialize<TR>(TR @event) where TR : class;
    }
}