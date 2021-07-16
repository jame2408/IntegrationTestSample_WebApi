using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using WebApi.Models.Weather.Response;
using WebApi.Service.Weather;
using WebApiTest.CommonLib;

namespace WebApiTest
{
    [TestFixture]
    // [Parallelizable(ParallelScope.All)]
    public class WeatherForecastTests : IntegrationTestBase
    {
        [Test]
        public async Task WeatherForecast_NoMock()
        {
            var message = await CreateHttpClient().GetAsync("/WeatherForecast");
            var list = await message.Content.ReadAsAsync<List<WeatherForecastResponse>>();

            message.StatusCode.Should().Be(HttpStatusCode.OK);
            list.Count.Should().Be(5);
        }

        [Test]
        public async Task WeatherForecast_Mock()
        {
            var service = Substitute.For<IWeatherForecastService>();
            service.WeatherForecasts().Returns(new List<WeatherForecastResponse>
            {
                new(),
                new()
            });

            ConfigureServices(collection => collection.AddScoped(_ => service));

            var message = await CreateHttpClient().GetAsync("/WeatherForecast");
            var list = await message.Content.ReadAsAsync<List<WeatherForecastResponse>>();

            message.StatusCode.Should().Be(HttpStatusCode.OK);
            list.Count.Should().Be(2);
        }
    }
}