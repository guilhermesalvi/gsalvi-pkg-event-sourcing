using System.Diagnostics.CodeAnalysis;

namespace GSalvi.EventSourcing.IntegrationTests.Configurations
{
    [ExcludeFromCodeCoverage]
    public class DummyEvent
    {
        public string DummyProp { get; set; }
    }
}