using System.Diagnostics.CodeAnalysis;
using GSalvi.EventSourcing.Tests.Configurations.Fakers;
using GSalvi.EventSourcing.Tests.Configurations.Models;
using Microsoft.Extensions.DependencyInjection;

namespace GSalvi.EventSourcing.Tests.Configurations;

[ExcludeFromCodeCoverage]
public class CustomEventDataTestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddEventSourcing<CustomEventData>(setup =>
        {
            setup.WithEventDataBuilder<CustomEventDataBuilder>();
            setup.Services.AddScoped<IEventDataRepository<CustomEventData>, FakeCustomEventDataRepository>();
        });

        services.AddLogging();
    }
}