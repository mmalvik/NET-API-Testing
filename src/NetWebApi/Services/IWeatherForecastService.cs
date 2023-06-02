namespace NetWebApi.Services;

public interface IWeatherForecastService
{
    Task<IEnumerable<WeatherForecast>> Get(int count);
}