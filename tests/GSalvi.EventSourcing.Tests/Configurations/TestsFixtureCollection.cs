using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace GSalvi.EventSourcing.Tests.Configurations;

[ExcludeFromCodeCoverage]
[CollectionDefinition(nameof(TestsFixtureCollection))]
public class TestsFixtureCollection :
    ICollectionFixture<TestHost<CustomEventDataTestStartup>>,
    ICollectionFixture<TestHost<DefaultEventDataTestStartup>>
{
}