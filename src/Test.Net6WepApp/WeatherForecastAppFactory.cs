using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Test.Net6WebApi;

internal class WeatherForecastAppFactory : WebApplicationFactory<Program>
{
    private readonly ITestOutputHelper _testOutputHelper;

    internal WeatherForecastAppFactory(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    protected override IHostBuilder? CreateHostBuilder()
    {
        var builder = Host.CreateDefaultBuilder();
        builder
            .ConfigureLogging(logBuilder =>
            {
                logBuilder
                    .SetMinimumLevel(LogLevel.Information)
                    .ClearProviders()
                    .AddProvider(new XunitLoggerProvider(_testOutputHelper));
            });

        return builder;
    }
}