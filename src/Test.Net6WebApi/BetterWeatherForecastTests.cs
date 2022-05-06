using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Net6WebApi;
using Xunit;
using Xunit.Abstractions;

namespace Test.Net6WebApi;

public class BetterWeatherForecastTests : TestBase
{
    public BetterWeatherForecastTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public async Task WhenRepositoryIsOk_ShouldReturnWeatherForecast()
    {
        var weatherForecast = await Get<IEnumerable<WeatherForecast>>("/weatherforecast");

        weatherForecast.Should().NotBeNull();
        weatherForecast.Should().HaveCount(5);
    }
}