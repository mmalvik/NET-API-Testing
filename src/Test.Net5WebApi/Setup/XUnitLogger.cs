using System;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Test.Net5WebApi.Setup
{
    /// <summary>
    /// A logger that logs to the xUnit test output.
    /// </summary>
    public class XunitLogger : ILogger
    {
        private readonly ITestOutputHelper _output;

        public XunitLogger(ITestOutputHelper output)
        {
            _output = output;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var log = $"{logLevel} {formatter(state, exception)}";

            if (exception != null)
            {
                log += $"{Environment.NewLine} + Exception: {exception}";
            }

            _output.WriteLine(log);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}