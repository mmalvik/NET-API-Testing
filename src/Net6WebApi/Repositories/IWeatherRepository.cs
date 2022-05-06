namespace Net6WebApi.Repositories;

public interface IWeatherRepository
{
    Task<IEnumerable<WeatherForecast>> GetAll();
}