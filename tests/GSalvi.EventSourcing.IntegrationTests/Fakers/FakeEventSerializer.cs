using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace GSalvi.EventSourcing.IntegrationTests.Fakers
{
    [ExcludeFromCodeCoverage]
    public class FakeEventSerializer : IEventSerializer
    {
        public string Serialize<TR>(TR @event) where TR : class
        {
            return JsonSerializer.Serialize(@event);
        }
    }
}