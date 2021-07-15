using System.Collections.Generic;
using WebApi.Models.Movie.Response;

namespace WebApi.Service.Movie
{
    public interface IMovieService
    {
        IEnumerable<MovieResponse> GetRestrictedMovies();
    }
}