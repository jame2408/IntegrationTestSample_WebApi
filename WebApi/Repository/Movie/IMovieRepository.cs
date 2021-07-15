using System.Collections.Generic;

namespace WebApi.Repository.Movie
{
    public interface IMovieRepository
    {
        IEnumerable<Database.Models.Movie> GetAll();
    }
}