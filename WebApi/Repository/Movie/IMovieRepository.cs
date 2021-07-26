using System.Collections.Generic;
using WebApi.Controllers;
using WebApi.Models.Movie.Request;

namespace WebApi.Repository.Movie
{
    public interface IMovieRepository
    {
        IEnumerable<Database.Models.Movie> GetAll();
        int Add(AddMovieRequest request);
    }
}