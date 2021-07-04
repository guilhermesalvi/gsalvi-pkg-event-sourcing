using System.Diagnostics.CodeAnalysis;

namespace GSalvi.EventSourcing.IntegrationTests.Configurations
{
    [ExcludeFromCodeCoverage]
    public class MyDummyEvent
    {
        public string UserId { get; set; }
    }
}