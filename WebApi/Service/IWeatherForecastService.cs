using System.Collections.Generic;

namespace WebApi.Service
{
    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> WeatherForecasts();
    }
}