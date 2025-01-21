using Api.Shared.Models;

namespace Api.Shared.Services.Services;

public interface IWeatherForecastService
{
    Task<IEnumerable<WeatherForecast>> Get(int count);
}