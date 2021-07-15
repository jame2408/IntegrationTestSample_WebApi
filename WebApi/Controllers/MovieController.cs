using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Movie.Response;
using WebApi.Service.Movie;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        /// <summary>
        /// 取得限制級電影清單
        /// </summary>
        /// <returns></returns>
        [HttpPost("Restricted")]
        public IEnumerable<MovieResponse> GetRestrictedMovies()
        {
            return this._movieService.GetRestrictedMovies() ?? new List<MovieResponse>();
        }
    }
}