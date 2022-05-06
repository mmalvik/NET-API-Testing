namespace Net6WebApi.Services;

public class WeatherService : IWeatherService
{
    private readonly ILogger<WeatherService> _logger;

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public WeatherService(ILogger<WeatherService> logger)
    {
        _logger = logger;
    }

    public Task<IEnumerable<WeatherForecast>> Get()
    {
        var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        });

        _logger.LogInformation("Got {NumberOfForecasts} WeatherForecasts from {Service}", forecasts.Count(), nameof(WeatherService));

        return Task.FromResult(forecasts);
    }
}