namespace Net6WebApi.Repositories;

public class WeatherRepository : IWeatherRepository
{
    public Task<IEnumerable<WeatherForecast>> Get(int count)
    {
        throw new NotImplementedException();
    }
}