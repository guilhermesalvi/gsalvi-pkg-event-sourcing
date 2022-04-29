using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mongo2Go;

namespace GSalvi.EventSourcing.Tests.Configurations;

[ExcludeFromCodeCoverage]
public class TestHost<TStartup> where TStartup : class
{
    public IConfiguration Configuration { get; }
    public IServiceProvider ServiceProvider { get; }

    public TestHost()
    {
        var configurationBuilder = new ConfigurationBuilder().Build();

        Configuration = configurationBuilder;

        var services = new ServiceCollection();
        services.AddSingleton(Configuration);
        services.AddScoped(_ => MongoDbRunner.Start());

        var startup = Activator.CreateInstance<TStartup>();

        startup.GetType().GetMethod("ConfigureServices")!.Invoke(startup, new object[] {services});

        ServiceProvider = services.BuildServiceProvider();
    }
}