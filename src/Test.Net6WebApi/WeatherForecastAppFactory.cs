using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Test.Net6WebApi.xUnit;
using Xunit.Abstractions;

namespace Test.Net6WebApi;

internal class WeatherForecastAppFactory : WebApplicationFactory<Program>
{
    private readonly ITestOutputHelper _testOutputHelper;

    internal WeatherForecastAppFactory(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<ILoggerFactory>();
        });
        builder.ConfigureLogging(logBuilder =>
        {
            logBuilder
                .SetMinimumLevel(LogLevel.Information)
                .ClearProviders()
                .AddProvider(new XunitLoggerProvider(_testOutputHelper));
        });
    }
}