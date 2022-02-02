using System.Diagnostics.CodeAnalysis;
using GSalvi.EventSourcing.Tests.Configurations.Fakers;
using Microsoft.Extensions.DependencyInjection;

namespace GSalvi.EventSourcing.Tests.Configurations;

[ExcludeFromCodeCoverage]
public class DefaultEventDataTestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddEventSourcing(setup =>
        {
            setup.Services.AddScoped<IEventDataRepository<EventData>, FakeDefaultEventDataRepository>();
        });

        services.AddLogging();
    }
}