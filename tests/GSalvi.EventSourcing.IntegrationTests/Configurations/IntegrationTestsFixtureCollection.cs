using Xunit;

namespace GSalvi.EventSourcing.IntegrationTests.Configurations
{
    [CollectionDefinition(nameof(IntegrationTestsFixtureCollection))]
    public class IntegrationTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<TestStartup>>
    {
    }
}