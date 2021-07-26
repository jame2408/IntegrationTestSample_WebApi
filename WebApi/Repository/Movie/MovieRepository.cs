using System;
using System.Collections.Generic;
using WebApi.Controllers;
using WebApi.Database;
using WebApi.Models.Movie.Request;

namespace WebApi.Repository.Movie
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieDbContext _dbContext;

        public MovieRepository(MovieDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IEnumerable<Database.Models.Movie> GetAll()
        {
            return this._dbContext.Movie;
        }

        public int Add(AddMovieRequest request)
        {
            var movie = new Database.Models.Movie()
            {
                Name = request.Name,
                Rating = request.Rating
            };
            _dbContext.Movie.Add(movie);
            _dbContext.SaveChanges();

            return movie.Id;
        }
    }
}