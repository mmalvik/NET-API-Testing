using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities;

public class WeatherForecastEntity
{
    public int Id { get; set; }

    public int Temperature { get; set; }
    
    [MaxLength(200)]
    public string Summary { get; set; } = string.Empty;
    
    public DateTime Date { get; set; }
}