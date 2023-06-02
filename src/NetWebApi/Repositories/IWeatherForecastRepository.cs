namespace NetWebApi.Repositories;

public interface IWeatherForecastRepository
{
    Task<IEnumerable<WeatherForecast>> Get(int count);
}