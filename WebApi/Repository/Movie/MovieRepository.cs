using System;
using System.Collections.Generic;
using WebApi.Database;

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
    }
}