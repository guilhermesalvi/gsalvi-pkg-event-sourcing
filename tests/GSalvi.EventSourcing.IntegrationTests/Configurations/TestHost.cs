using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GSalvi.EventSourcing.IntegrationTests.Configurations
{
    public class TestHost<T> where T : TestStartup
    {
        public IConfiguration Configuration { get; }
        public IServiceProvider ServiceProvider { get; }

        public TestHost()
        {
            var configurationBuilder =
                new ConfigurationBuilder()
                    .Build();

            Configuration = configurationBuilder;

            var services = new ServiceCollection();
            services.AddSingleton(Configuration);

            var startup = Activator.CreateInstance<T>();

            startup.ConfigureServices(services);

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}