using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace Test.Net6WebApi;

public class TestBase
{
    private readonly AppFactory _appFactory;
    private HttpClient _httpClient;

    public TestBase(ITestOutputHelper testOutputHelper)
    {
        _appFactory = new AppFactory(testOutputHelper);
    }

    protected void ConfigureTestServices(Action<IServiceCollection> serviceCollectionAction)
    {
        EnsureTestServerNotRunning();
        _appFactory.ConfigureTestServices = serviceCollectionAction;
    }

    protected async Task<T> Get<T>(string url)
    {
        return await Client.GetFromJsonAsync<T>(url);
    }

    protected async Task<HttpResponseMessage> Get(string url)
    {
        return await Client.GetAsync(url);
    }

    private HttpClient Client
    {
        get
        {
            if (_httpClient != null)
            {
                return _httpClient;
            }
            _httpClient = _appFactory.CreateClient();
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