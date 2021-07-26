using System.Collections.Generic;
using WebApi.Controllers;
using WebApi.Models.Movie.Request;
using WebApi.Models.Movie.Response;

namespace WebApi.Service.Movie
{
    public interface IMovieService
    {
        IEnumerable<MovieResponse> GetRestrictedMovies();
        int AddMovie(AddMovieRequest request);
    }
}