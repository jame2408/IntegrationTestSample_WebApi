using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using WebApi;
using WebApi.Service;
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
            var list = await message.Content.ReadAsAsync<List<WeatherForecast>>();

            message.StatusCode.Should().Be(HttpStatusCode.OK);
            list.Count.Should().Be(5);
        }

        [Test]
        public async Task WeatherForecast_Mock()
        {
            var service = Substitute.For<IWeatherForecastService>();
            service.WeatherForecasts().Returns(new List<WeatherForecast>
            {
                new(),
                new()
            });

            var message = await CreateHttpClient(collection => collection.AddScoped(_ => service))
                .GetAsync("/WeatherForecast");
            var list = await message.Content.ReadAsAsync<List<WeatherForecast>>();

            message.StatusCode.Should().Be(HttpStatusCode.OK);
            list.Count.Should().Be(2);
        }
    }
}