﻿using Api.Shared.Models;

namespace Api.Shared.Repositories.Repositories;

public interface IWeatherForecastRepository
{
    Task<IEnumerable<WeatherForecast>> Get(int count);
}