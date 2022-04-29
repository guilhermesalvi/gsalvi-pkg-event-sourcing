using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace GSalvi.EventSourcing.Tests.DefaultEventDataImplementation;

[ExcludeFromCodeCoverage]
public class TestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddEventSourcing(setup =>
        {
            setup.Services.AddScoped<IEventDataRepository<EventData>, Repository>();
        });

        services.AddLogging();
    }
}