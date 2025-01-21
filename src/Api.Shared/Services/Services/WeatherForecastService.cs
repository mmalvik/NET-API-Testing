using Api.Shared.Models;
using Api.Shared.Repositories.Repositories;
using Microsoft.Extensions.Logging;

namespace Api.Shared.Services.Services;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly IWeatherForecastRepository _weatherForecastRepository;
    private readonly ILogger<WeatherForecastService> _logger;

    public WeatherForecastService(IWeatherForecastRepository weatherForecastRepository, ILogger<WeatherForecastService> logger)
    {
        _weatherForecastRepository = weatherForecastRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<WeatherForecast>> Get(int count)
    {
        var forecasts = await _weatherForecastRepository.Get(count);

        if (forecasts.Any(weatherForecast => weatherForecast.TemperatureC < 0))
        {
            _logger.LogWarning("Some weather data has temperature below 0");
        }

        _logger.LogInformation("Got {NumberOfForecasts} WeatherForecasts from {Service}", forecasts.Count(), nameof(WeatherForecastService));

        return forecasts;
    }
}