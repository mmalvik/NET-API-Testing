using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NetWebApi;
using NetWebApi.Repositories;
using Test.NetWebApi.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace Test.NetWebApi;

public class BetterWeatherForecastApiTests : ApiTestBase
{
    private readonly Mock<IWeatherForecastRepository> _weatherRepositoryMock;

    public BetterWeatherForecastApiTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _weatherRepositoryMock = new Mock<IWeatherForecastRepository>();
    }

    [Fact]
    public async Task WhenCountNotProvided_ShouldReturnBadRequest()
    {
        var response = await Get("/weatherforecast");

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task WhenCountNotANumber_ShouldReturnBadRequest()
    {
        var response = await Get("/weatherforecast?count=hello");

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task WhenCountIsProvided_ShouldReturnCorrectNumberOfItems()
    {
        var numberOfItems = 10;
        _weatherRepositoryMock.Setup(x => x.Get(numberOfItems))
            .ReturnsAsync(GetFakeData(numberOfItems));
        ConfigureTestServices(services =>
        {
            services.AddTransient(_ => _weatherRepositoryMock.Object);
        });

        var response = await Get<IEnumerable<WeatherForecast>>($"/weatherforecast?count={ numberOfItems }");

        response.Should().HaveCount(numberOfItems);
    }

    /// <summary>
    /// Return fake weather forecasts.
    /// </summary>
    /// <param name="count">The number of items to return</param>
    /// <returns></returns>
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