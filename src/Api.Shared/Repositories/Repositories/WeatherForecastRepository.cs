using Api.Shared.Models;
using DataLayer;
using Microsoft.EntityFrameworkCore;

namespace Api.Shared.Repositories.Repositories;

public class WeatherForecastRepository : IWeatherForecastRepository
{
    private readonly WeatherForecastContext _weatherForecastContext;

    public WeatherForecastRepository(WeatherForecastContext weatherForecastContext)
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