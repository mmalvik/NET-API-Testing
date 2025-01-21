using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Api.Shared.Models;
using Api.Shared.Repositories.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NetWebApi;
using Xunit;
using Xunit.Abstractions;

namespace Test.NetWebApi
{
    public class WeatherForecastTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public WeatherForecastTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task WhenNoWeatherForecasts_ShouldReturnEmptyList()
        {
            var weatherRepositoryMock = new Mock<IWeatherForecastRepository>();
            weatherRepositoryMock.Setup(x => x.Get(It.IsAny<int>()))
                .ReturnsAsync(new List<WeatherForecast>());

            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        services.AddTransient(_ => weatherRepositoryMock.Object);
                    });

                });

            var client = application.CreateClient();

            var response = await client.GetFromJsonAsync<IEnumerable<WeatherForecast>>("/weatherforecast?count=10");

            response.Should().NotBeNull();
            response.Should().BeEmpty();
        }
    }
}
