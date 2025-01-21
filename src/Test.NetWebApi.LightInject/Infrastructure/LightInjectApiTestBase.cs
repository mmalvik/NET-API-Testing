using System.Net.Http.Json;
using LightInject;
using Xunit.Abstractions;

namespace Test.NetWebApi.LightInject.Infrastructure;

/// <summary>
/// A test base class for handling cross cutting concerns.
/// </summary>
public class LightInjectApiTestBase
{
    private readonly LightInjectWebAppFactory _webAppFactory;
    private HttpClient _httpClient;

    public LightInjectApiTestBase(ITestOutputHelper testOutputHelper)
    {
        _webAppFactory = new LightInjectWebAppFactory(testOutputHelper);
    }

    protected void ConfigureDependencyInjectionContainer(Action<IServiceContainer> containerAction)
    {
        EnsureTestServerNotRunning();
        _webAppFactory.ConfigureDependencyInjectionContainer = containerAction;
    }

    protected async Task<T> Get<T>(string url)
    {
        return await Client.GetFromJsonAsync<T>(url);
    }

    protected async Task<HttpResponseMessage> Get(string url)
    {
        return await Client.GetAsync(url);
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
            _httpClient = _webAppFactory.CreateClient();
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