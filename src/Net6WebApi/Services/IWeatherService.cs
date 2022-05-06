namespace Net6WebApi.Services;

public interface IWeatherService
{
    Task<IEnumerable<WeatherForecast>> Get(int count);
}