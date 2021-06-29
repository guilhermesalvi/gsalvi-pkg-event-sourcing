using System.Text.Json;

namespace GSalvi.EventSourcing.IntegrationTests.Fakers
{
    public class FakeEventSerializer : IEventSerializer
    {
        public string Serialize<TR>(TR @event) where TR : class
        {
            return JsonSerializer.Serialize(@event);
        }
    }
}