using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Test.Net6WebApi.xUnit;
using Xunit.Abstractions;

namespace Test.Net6WebApi;

internal class AppFactory : WebApplicationFactory<Program>
{
    private readonly ITestOutputHelper _testOutputHelper;

    internal AppFactory(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    internal Action<IServiceCollection> ConfigureTestServices { get; set; }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        //builder.ConfigureServices(services =>
        //{
        //    services.RemoveAll<ILoggerFactory>();
        //});
        builder.ConfigureTestServices(services =>
        {
            ConfigureTestServices?.Invoke(services);
        });
        //builder.ConfigureLogging(logBuilder =>
        //{
        //    logBuilder
        //        .SetMinimumLevel(LogLevel.Information)
        //        .ClearProviders()
        //        .AddProvider(new XunitLoggerProvider(_testOutputHelper));
        //});
    }
}