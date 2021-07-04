using System.Diagnostics.CodeAnalysis;

namespace GSalvi.EventSourcing.UnitTests.Configurations
{
    [ExcludeFromCodeCoverage]
    public class MySnapshot : Snapshot
    {
        public string UserId { get; set; }
    }
}