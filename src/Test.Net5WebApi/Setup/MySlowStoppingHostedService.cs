using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Test.Net5WebApi.Setup
{
    public class MySlowStoppingHostedService : IHostedService
    {
        private readonly TelemetryClient _telemetryClient;

        public MySlowStoppingHostedService(TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            Log.Information("Flushing");
            Log.CloseAndFlush();
            _telemetryClient.Flush();
            Log.Information("Waiting for flush");
            await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
            Log.Information("Done waiting for flush");
        }
    }
}