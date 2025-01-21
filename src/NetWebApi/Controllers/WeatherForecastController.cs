using Api.Shared.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace NetWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(IWeatherForecastService weatherForecastService, ILogger<WeatherForecastController> logger) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int count)
        {
            logger.LogInformation("Begin - Getting weather forecast");

            if (count == 0 || count > 100)
            {
                logger.LogError("Invalid count of {Count} provided", count);
                return BadRequest();
            }

            var weatherForecasts = await weatherForecastService.Get(count);

            logger.LogInformation("End - Getting weather forecast");
            return Ok(weatherForecasts);
        }
    }
}