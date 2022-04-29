using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace GSalvi.EventSourcing.Tests.CustomEventDataImplementation;

[ExcludeFromCodeCoverage]
public class TestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddEventSourcing<CustomEventData>(setup =>
        {
            setup.WithEventDataBuilder<CustomEventDataBuilder>();
            setup.Services.AddScoped<IEventDataRepository<CustomEventData>, Repository>();
        });

        services.AddLogging();
    }
}