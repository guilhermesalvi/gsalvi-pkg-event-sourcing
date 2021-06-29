using System.Runtime.CompilerServices;
using System.Text.Json;

[assembly: InternalsVisibleTo("GSalvi.EventSourcing.UnitTests")]

namespace GSalvi.EventSourcing
{
    internal class EventSerializer : IEventSerializer
    {
        public string Serialize<TR>(TR @event) where TR : class
        {
            return JsonSerializer.Serialize(@event);
        }
    }
}