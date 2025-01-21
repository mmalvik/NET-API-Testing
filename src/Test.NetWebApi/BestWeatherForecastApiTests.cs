using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Shared.Models;
using FluentAssertions;
using Test.NetWebApi.Infrastructure;
using Test.Shared;
using Xunit;
using Xunit.Abstractions;

namespace Test.NetWebApi;

public class BestWeatherForecastApiTests : ApiTestBaseWithDatabase
{
    public BestWeatherForecastApiTests(ITestOutputHelper testOutputHelper, SqlServerTestFixture sqlServerTestFixture) : base(testOutputHelper, sqlServerTestFixture)
    {
    }
    
    [Fact]
    public async Task WhenCountIsNotProvided_ShouldReturnBadRequest()
    {
        var response = await Get($"/weatherforecast");

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task WhenOneItemRequested_ShouldReturnOneItem()
    {
        var numberOfItems = 1;
        
        var response = await Get<IEnumerable<WeatherForecast>>($"/weatherforecast?count={ numberOfItems }");

        response.First().Summary.Should().Be("Sunny");
    }
    
    [Fact]
    public async Task WhenFiveItemsRequested_ShouldReturnFiveItems()
    {
        var numberOfItems = 5;
        
        var response = await Get<IEnumerable<WeatherForecast>>($"/weatherforecast?count={ numberOfItems }");

        response.Count().Should().Be(numberOfItems);
    }
}