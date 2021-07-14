using Microsoft.EntityFrameworkCore;
using WebApi.Database.Models;

namespace WebApi.Database
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasKey(r => r.Id);
            modelBuilder.Entity<Movie>().Property(r => r.Name).HasColumnType("nvarchar(20)");
        }
    }
}