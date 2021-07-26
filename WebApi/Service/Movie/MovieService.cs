using System.Collections.Generic;
using System.Linq;
using WebApi.Controllers;
using WebApi.Models.Movie.Request;
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
            var restrictedMovies = _movieRepository
                .GetAll()
                .Where(p => p.Rating == (int) MovieRatingEnum.Restricted);

            return restrictedMovies.Select(s => new MovieResponse()
            {
                Name = s.Name
            });
        }

        public int AddMovie(AddMovieRequest request)
        {
            return _movieRepository.Add(request);
        }
    }

    public enum MovieRatingEnum
    {
        Restricted = 18
    }
}