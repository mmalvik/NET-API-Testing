using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Net6WebApi;
using Test.Net6WebApi.xUnit;
using Xunit;
using Xunit.Abstractions;

namespace Test.Net6WebApi
{
    public class WeatherForecastTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public WeatherForecastTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task ShouldGetWeatherForecast()
        {
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
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
                });

            var client = application.CreateClient();

            var response = await client.GetFromJsonAsync<IEnumerable<WeatherForecast>>("/weatherforecast");

            Assert.NotEmpty(response);
        }

        [Fact]
        public async Task ShouldGetWeatherForecastWithCustomWebAppFactory()
        {
            var application = new WeatherForecastAppFactory(_testOutputHelper);

            var client = application.CreateClient();

            var response = await client.GetFromJsonAsync<IEnumerable<WeatherForecast>>("/weatherforecast");

            Assert.NotEmpty(response);
        }
    }
}
