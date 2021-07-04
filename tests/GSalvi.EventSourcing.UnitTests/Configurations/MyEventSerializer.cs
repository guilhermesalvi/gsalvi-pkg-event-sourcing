using System.Diagnostics.CodeAnalysis;

namespace GSalvi.EventSourcing.UnitTests.Configurations
{
    [ExcludeFromCodeCoverage]
    public class MyEventSerializer : IEventSerializer
    {
        public string Serialize<TR>(TR @event) where TR : class
        {
            return string.Empty;
        }
    }
}