#nullable enable
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace Test.Net6WebApi.Setup
{
    public sealed class MyWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.ConfigureServices(services =>
            {
                services.RemoveAll<ILoggerFactory>();
                services.AddApplicationInsightsTelemetry();
                services.AddHostedService<MySlowStoppingHostedService>();
            });
        }
    }
}