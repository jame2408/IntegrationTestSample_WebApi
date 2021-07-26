using WebApi.Database;
using WebApiTest.SeedData.Movie;

namespace WebApiTest.DatabaseConfig.Movie
{
    public class MovieTestDbData
    {
        public void AddDefaultMovies(MovieDbContext context)
        {
            context?.Movie.AddRange(MoviesEntities.DefaultMoviesEntity());
            context?.SaveChanges();
        }
    }
}