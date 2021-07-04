using System.Diagnostics.CodeAnalysis;

namespace GSalvi.EventSourcing.IntegrationTests.Configurations
{
    [ExcludeFromCodeCoverage]
    public class MySnapshot : Snapshot
    {
        public string UserId { get; set; }
    }
}