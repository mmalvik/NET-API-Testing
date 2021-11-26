using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Net6WebApi;
using Xunit;

namespace Test.Net6WebApi.Setup
{
    internal static class MyTest
    {
        internal static async Task RunTest(MyWebApplicationFactory factory)
        {
            var client = factory.CreateClient();

            var response = await client.GetFromJsonAsync<IEnumerable<WeatherForecast>>("/weatherforecast");

            // await Task.Delay(TimeSpan.FromSeconds(0.2));
            Assert.NotEmpty(response);
        }
    }
}