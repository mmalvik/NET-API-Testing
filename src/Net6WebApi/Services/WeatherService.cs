using Net6WebApi.Repositories;

namespace Net6WebApi.Services;

public class WeatherService : IWeatherService
{
    private readonly IWeatherRepository _weatherRepository;
    private readonly ILogger<WeatherService> _logger;

    public WeatherService(IWeatherRepository weatherRepository, ILogger<WeatherService> logger)
    {
        _weatherRepository = weatherRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<WeatherForecast>> Get(int count)
    {
        var forecasts = await _weatherRepository.Get(count);

        if (forecasts.Any(weatherForecast => weatherForecast.TemperatureC < 0))
        {
            _logger.LogWarning("Some weather data has temperature below 0");
        }

        _logger.LogInformation("Got {NumberOfForecasts} WeatherForecasts from {Service}", forecasts.Count(), nameof(WeatherService));

        return forecasts;
    }
}