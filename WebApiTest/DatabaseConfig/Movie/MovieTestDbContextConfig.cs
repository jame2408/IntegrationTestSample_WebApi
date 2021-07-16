using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Database;

namespace WebApiTest.DatabaseConfig.Movie
{
    public class MovieTestDbContextConfig : ITestDbContextConfig
    {
        public void Set(IServiceCollection services)
        {
            // Remove the app's MovieDbContext registration.
            var descriptor =
                services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<MovieDbContext>));
            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            // Add MovieDbContext using an in-memory database for testing.
            services.AddDbContext<MovieDbContext>(options => { options.UseInMemoryDatabase(nameof(MovieDbContext)); });

            // Build the service provider.
            var serviceProvider = services.BuildServiceProvider();
            // Create a scope to obtain a reference to the database
            // context (MovieDbContext).
            using var scope = serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
            // Ensure the database is deleted.
            db.Database.EnsureDeleted();
            // Ensure the database is created.
            db.Database.EnsureCreated();

            InitDb(db);
        }

        private static void InitDb(MovieDbContext db)
        {
            db.Movie.AddRange(new List<WebApi.Database.Models.Movie>()
            {
                new() {Name = "復仇者聯盟：終局之戰", Rating = 6},
                new() {Name = "黑寡婦", Rating = 12},
                new() {Name = "詭屋", Rating = 18},
                new() {Name = "死亡漩渦：奪魂鋸新遊戲", Rating = 18},
                new() {Name = "玩命鈔劫", Rating = 15},
                new() {Name = "尋龍使者：拉雅", Rating = 0},
                new() {Name = "德州電鋸殺人狂", Rating = 18}
            });
            db.SaveChanges();
        }
    }
}