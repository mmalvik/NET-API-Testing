using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
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

    protected async Task<T> Get<T>(string url)
    {
        return await Client.GetFromJsonAsync<T>(url);
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
}