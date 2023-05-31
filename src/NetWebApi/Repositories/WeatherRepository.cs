namespace NetWebApi.Repositories;

public class WeatherRepository : IWeatherRepository
{
    public Task<IEnumerable<WeatherForecast>> Get(int count)
    {
        throw new NotImplementedException();
    }
}