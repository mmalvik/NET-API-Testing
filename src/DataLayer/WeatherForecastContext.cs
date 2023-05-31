using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer;

public class WeatherForecastContext : DbContext
{
    public WeatherForecastContext(DbContextOptions<WeatherForecastContext> options) : base(options) { }

    public DbSet<WeatherForecast> WeatherForecasts { get; set; }
}