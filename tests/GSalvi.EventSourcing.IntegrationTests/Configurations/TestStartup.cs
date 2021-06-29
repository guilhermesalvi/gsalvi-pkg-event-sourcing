using GSalvi.EventSourcing.IntegrationTests.Fakers;
using Microsoft.Extensions.DependencyInjection;

namespace GSalvi.EventSourcing.IntegrationTests.Configurations
{
    public class TestStartup
    {
        public void ConfigureServices(
            IServiceCollection services)
        {
            services.AddEventSourcing<MySnapshot>(setupAction =>
            {
                setupAction.WithSerializer<FakeEventSerializer>();
                setupAction.WithSnapshotBuilder<FakeSnapshotBuilder, MySnapshot>();
                setupAction.Services.AddScoped<ISnapshotRepository<MySnapshot>, FakeSnapshotRepository>();
            });
        }
    }
}