using Api.Shared.Models;
using Api.Shared.Repositories.Repositories;
using FluentAssertions;
using Moq;
using Test.NetWebApi.LightInject.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace Test.NetWebApi.LightInject;

public class ApiTests : LightInjectApiTestBase
{
    private readonly Mock<IWeatherForecastRepository> _weatherRepositoryMock;
    
    public ApiTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _weatherRepositoryMock = new Mock<IWeatherForecastRepository>();
    }
    
    [Fact]
    public async Task WhenCountIsProvided_ShouldReturnCorrectNumberOfItems()
    {
        var numberOfItems = 10;
        _weatherRepositoryMock.Setup(x => x.Get(numberOfItems))
            .ReturnsAsync(GetFakeData(numberOfItems));
        ConfigureDependencyInjectionContainer(c =>
        {
            c.Register(_ => _weatherRepositoryMock.Object);
        });

        var response = await Get<IEnumerable<WeatherForecast>>($"/weatherforecast?count={ numberOfItems }");

        _weatherRepositoryMock.Verify(x => x.Get(numberOfItems), Times.Once);
        response.Should().HaveCount(numberOfItems);
    }
    
    /// <summary>
    /// Returns fake weather forecasts.
    /// </summary>
    /// <param name="count">The number of items to return</param>
    private IEnumerable<WeatherForecast> GetFakeData(int count)
    {
        return Enumerable.Range(count, count)
            .Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-50, 60),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            });
    }

    private static readonly string[] Summaries = {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
}