using DataLayer;
using Microsoft.EntityFrameworkCore;

namespace NetWebApi.Repositories;

public class WeatherRepository : IWeatherRepository
{
    private readonly WeatherForecastContext _weatherForecastContext;

    public WeatherRepository(WeatherForecastContext weatherForecastContext)
    {
        _weatherForecastContext = weatherForecastContext;
    }
    
    public async Task<IEnumerable<WeatherForecast>> Get(int count)
    {
        var weatherForecasts = await _weatherForecastContext.WeatherForecasts.Take(count)
            .Select(wf => new WeatherForecast
            {
                Date = wf.Date,
                TemperatureC = wf.Temperature,
                Summary = wf.Summary
            })
            .ToListAsync();
        
        return weatherForecasts;
    }
}