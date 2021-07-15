using System.Collections.Generic;
using WebApi.Models.Weather.Response;

namespace WebApi.Service.Weather
{
    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecastResponse> WeatherForecasts();
    }
}