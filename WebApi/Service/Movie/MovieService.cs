using System.Collections.Generic;
using System.Linq;
using WebApi.Models.Movie.Response;
using WebApi.Repository.Movie;

namespace WebApi.Service.Movie
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public IEnumerable<MovieResponse> GetRestrictedMovies()
        {
            return _movieRepository
                .GetAll()
                .Where(p => p.Rating == (int) MovieRatingEnum.Restricted)
                .Select(s => new MovieResponse(s));
        }
    }

    public enum MovieRatingEnum
    {
        Restricted = 18
    }
}