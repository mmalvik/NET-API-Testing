﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Test.Shared.xUnit;
using Xunit.Abstractions;

namespace Test.NetWebApi.Infrastructure;

/// <summary>
/// A custom WebApplicationFactory with built-in logging to test output.
/// </summary>
internal class WeatherForecastAppFactory : WebApplicationFactory<Program>
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly Dictionary<string, string> _overriddenAppsettings;
    
    internal WeatherForecastAppFactory(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _overriddenAppsettings = new Dictionary<string, string>();
    }

    internal Action<IServiceCollection> ConfigureTestServices { get; set; }

    internal Dictionary<string, string> OverriddenAppsettings => _overriddenAppsettings;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configBuilder =>
        {
            configBuilder.AddInMemoryCollection(OverriddenAppsettings);
        });
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<ILoggerFactory>();
        });
        builder.ConfigureTestServices(services =>
        {
            ConfigureTestServices?.Invoke(services);
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