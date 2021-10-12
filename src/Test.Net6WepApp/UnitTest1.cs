using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Net6WepApp;
using Xunit;
using Xunit.Abstractions;

namespace Test.Net6WepApp
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public UnitTest1(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task HelloWorld()
        {
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureLogging(logBuilder =>
                    {
                        logBuilder
                            .SetMinimumLevel(LogLevel.Information)
                            .ClearProviders()
                            .AddProvider(new XunitLoggerProvider(_testOutputHelper));
                    });
                });

            var client = application.CreateClient();

            var response = await client.GetFromJsonAsync<IEnumerable<WeatherForecast>>("/api/weatherforecast");

            Assert.NotEmpty(response);
        }
    }
}
