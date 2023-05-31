namespace NetWebApi.Repositories;

public interface IWeatherRepository
{
    Task<IEnumerable<WeatherForecast>> Get(int count);
}