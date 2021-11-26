#nullable enable
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Net5WebApi;

namespace Test.Net5WebApi.Setup
{
    public sealed class MyWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override IHostBuilder CreateHostBuilder() => Host
            .CreateDefaultBuilder()
            .ConfigureWebHostDefaults(webHost => webHost.UseStartup<Startup>());

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