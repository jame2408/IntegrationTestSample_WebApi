using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using WebApi.Models.Movie.Response;
using WebApiTest.CommonLib;

namespace WebApiTest
{
    [TestFixture]
    public class MovieControllerTest : IntegrationTestBase
    {
        [Test]
        public async Task GetRestrictedMovies()
        {
            base.MovieDbConfig();

            var message = await base.CreateHttpClient().PostAsync("/Movie/Restricted",
                new StringContent("", Encoding.UTF8, "application/json"));
            var restrictedMovies = await message.Content.ReadAsAsync<List<MovieResponse>>();

            message.StatusCode.Should().Be(HttpStatusCode.OK);
            restrictedMovies.Select(s => s.Name).Should().BeEquivalentTo(
                "死亡漩渦：奪魂鋸新遊戲", "詭屋", "德州電鋸殺人狂");
        }
    }
}