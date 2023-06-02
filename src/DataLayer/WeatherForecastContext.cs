using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer;

public class WeatherForecastContext : DbContext
{
    public WeatherForecastContext(DbContextOptions<WeatherForecastContext> options) : base(options) { }

    public DbSet<WeatherForecastEntity> WeatherForecasts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WeatherForecastEntity>().HasData(
            new WeatherForecastEntity { Id = 1, Temperature = 20, Summary = "Sunny", Date = DateTime.Now },
            new WeatherForecastEntity { Id = 2, Temperature = 22, Summary = "Partly Cloudy", Date = DateTime.Now.AddDays(1) },
            new WeatherForecastEntity { Id = 3, Temperature = 25, Summary = "Cloudy", Date = DateTime.Now.AddDays(2) },
            new WeatherForecastEntity { Id = 4, Temperature = 24, Summary = "Rainy", Date = DateTime.Now.AddDays(3) },
            new WeatherForecastEntity { Id = 5, Temperature = 21, Summary = "Sunny", Date = DateTime.Now.AddDays(4) }
        );
    }
}