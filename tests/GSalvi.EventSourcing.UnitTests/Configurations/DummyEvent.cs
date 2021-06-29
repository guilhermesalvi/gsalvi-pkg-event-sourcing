using System.Diagnostics.CodeAnalysis;

namespace GSalvi.EventSourcing.UnitTests.Configurations
{
    [ExcludeFromCodeCoverage]
    public class DummyEvent
    {
        public string DummyProp { get; set; }
    }
}