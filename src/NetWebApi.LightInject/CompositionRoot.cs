using Api.Shared.Repositories.Repositories;
using Api.Shared.Services.Services;
using LightInject;

namespace NetWebApi.LightInject;

public class CompositionRoot : ICompositionRoot
{
    public void Compose(IServiceRegistry serviceRegistry)
    {
        serviceRegistry.Register<IWeatherForecastRepository, WeatherForecastRepository>();
        serviceRegistry.Register<IWeatherForecastService, WeatherForecastService>();
    }
}