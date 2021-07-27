using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using WebApi.Controllers;
using WebApi.Database;
using WebApi.Models.Movie.Request;
using WebApi.Models.Movie.Response;
using WebApi.Repository.Movie;
using WebApi.Service.Movie;
using WebApiTest.CommonLib;
using WebApiTest.DatabaseConfig.Movie;
using WebApiTest.SeedData.Movie;

namespace WebApiTest
{
    [TestFixture]
    public class MovieControllerTest : IntegrationTestBase
    {
        [SetUp]
        public void SetUp()
        {
            base.DbContextConfig<MovieDbContext>();
        }

        [Test]
        public async Task AddMovies()
        {
            var request = new AddMovieRequest()
            {
                Name = "格雷的五十道陰影",
                Rating = 18
            };
            var message = await base.CreateHttpClient().PostAsync("/Movie/Add",
                new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json"));
            var response = await message.Content.ReadAsAsync<int>();

            message.StatusCode.Should().Be(HttpStatusCode.OK);
            base.DbOperator<MovieDbContext>(context =>
            {
                var movie = context.Movie.Find(response);
                movie.Name.Should().Be("格雷的五十道陰影");
                movie.Rating.Should().Be(18);
            });
        }

        [Test]
        public async Task GetRestrictedMovies()
        {
            base.DbOperator<MovieDbContext>(context => { new MovieTestDbData().AddDefaultMovies(context); });

            var message = await base.CreateHttpClient().PostAsync("/Movie/Restricted",
                new StringContent("", Encoding.UTF8, "application/json"));
            var restrictedMovies = await message.Content.ReadAsAsync<List<MovieResponse>>();

            message.StatusCode.Should().Be(HttpStatusCode.OK);
            restrictedMovies.Select(s => s.Name).Should().BeEquivalentTo(
                "死亡漩渦：奪魂鋸新遊戲", "詭屋", "德州電鋸殺人狂");
        }

        [Test]
        public async Task GetRestrictedMovies_StubRepository()
        {
            var repository = Substitute.For<IMovieRepository>();
            repository.GetAll().Returns(MoviesEntities.DefaultMoviesEntity());

            #region 寫法一：直接打 API
            //ConfigureServices(service => service.AddScoped(_ => repository));

            //var message = await base.CreateHttpClient().PostAsync("/Movie/Restricted",
            //    new StringContent("", Encoding.UTF8, "application/json"));
            //var restrictedMovies = await message.Content.ReadAsAsync<List<MovieResponse>>();

            //message.StatusCode.Should().Be(HttpStatusCode.OK);
            #endregion

            #region 寫法二：new Controller
            var movieController = new MovieController(new MovieService(repository));
            var restrictedMovies = movieController.GetRestrictedMovies();
            #endregion

            restrictedMovies.Select(s => s.Name).Should().BeEquivalentTo(
                "死亡漩渦：奪魂鋸新遊戲", "詭屋", "德州電鋸殺人狂");
        }

        [TearDown]
        public void TearDown()
        {
            base.DbClean<MovieDbContext>();
        }
    }
}