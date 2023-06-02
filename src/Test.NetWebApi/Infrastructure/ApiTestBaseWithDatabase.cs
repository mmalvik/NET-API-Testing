using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

namespace Test.NetWebApi.Infrastructure;

/// <summary>
/// A test base class for handling cross cutting concerns for API tests.
/// </summary>
/// <remarks>
/// Most of this is duplicated from <see cref="ApiTestBase"/> but this class also starts a test database.
/// This is just for demonstration purposes. In a real project you would probably use a real database.
/// </remarks>
[Collection(nameof(ApiTestCollection))]
public class ApiTestBaseWithDatabase
{
    private readonly WeatherForecastAppFactory _weatherForecastAppFactory;
    private HttpClient _httpClient;

    public ApiTestBaseWithDatabase(ITestOutputHelper testOutputHelper, SqlServerTestFixture sqlServerTestFixture)
    {
        _weatherForecastAppFactory = new WeatherForecastAppFactory(testOutputHelper);
        OverrideAppsettings("ConnectionStrings:WeatherForecasts", sqlServerTestFixture.ConnectionString);
    }

    protected async Task<T> Get<T>(string url)
    {
        return await Client.GetFromJsonAsync<T>(url);
    }

    protected async Task<HttpResponseMessage> Get(string url)
    {
        return await Client.GetAsync(url);
    }
    
    protected void OverrideAppsettings(string key, string value)
    {
        EnsureTestServerNotRunning();
        _weatherForecastAppFactory.OverriddenAppsettings.Add(key, value);
    }

    /// <remarks>
    /// _weatherForecastAppFactory.CreateClient() actually starts the test server.
    /// </remarks>
    private HttpClient Client
    {
        get
        {
            if (_httpClient != null)
            {
                return _httpClient;
            }
            _httpClient = _weatherForecastAppFactory.CreateClient();
            return _httpClient;
        }
    }

    /// <summary>
    /// Helper method to make sure the test server in not already running when trying to configure it.
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    private void EnsureTestServerNotRunning()
    {
        if (_httpClient != null)
        {
            throw new InvalidOperationException("Test server has already started, cannot configure setup now");
        }
    }
}