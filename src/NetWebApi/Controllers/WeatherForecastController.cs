using Microsoft.AspNetCore.Mvc;
using NetWebApi.Services;

namespace NetWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IWeatherService weatherService, ILogger<WeatherForecastController> logger)
        {
            _weatherService = weatherService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int count)
        {
            _logger.LogInformation("Begin - Getting weather forecast");

            if (count == default || count > 100)
            {
                _logger.LogError("Invalid count of {Count} provided", count);
                return BadRequest();
            }

            var weatherForecasts = await _weatherService.Get(count);

            _logger.LogInformation("End - Getting weather forecast");
            return Ok(weatherForecasts);
        }
    }
}