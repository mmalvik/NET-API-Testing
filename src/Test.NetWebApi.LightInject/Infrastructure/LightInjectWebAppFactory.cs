using LightInject;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Test.Shared.xUnit;
using Xunit.Abstractions;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace Test.NetWebApi.LightInject.Infrastructure;

/// <summary>
/// A custom WebApplicationFactory with built-in logging to test output.
/// </summary>
internal class LightInjectWebAppFactory : WebApplicationFactory<Program>
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly Dictionary<string, string> _overriddenAppsettings;
    
    internal LightInjectWebAppFactory(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _overriddenAppsettings = new Dictionary<string, string>();
    }

    internal Action<IServiceContainer> ConfigureDependencyInjectionContainer { get; set; }

    internal Dictionary<string, string> OverriddenAppsettings => _overriddenAppsettings;

    
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configBuilder =>
        {
            configBuilder.AddInMemoryCollection(OverriddenAppsettings);
        });
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<ILoggerFactory>();
        });
        builder.ConfigureLogging(logging =>
        {
            logging.ClearProviders()
                .SetMinimumLevel(LogLevel.Information)
                .AddProvider(new XunitLoggerProvider(_testOutputHelper));
        });
        builder.ConfigureContainer<IServiceContainer>(container =>
        {
            ConfigureDependencyInjectionContainer(container);
        });

        return base.CreateHost(builder);
    }
}