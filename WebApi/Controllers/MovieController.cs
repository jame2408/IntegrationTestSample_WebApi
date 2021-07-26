using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Movie.Request;
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

        /// <summary>
        /// 新增一筆電影資訊
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public int AddMovie(AddMovieRequest request)
        {
            return this._movieService.AddMovie(request);
        }
    }
}