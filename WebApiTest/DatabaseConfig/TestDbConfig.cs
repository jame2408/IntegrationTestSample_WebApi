using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace WebApiTest.DatabaseConfig
{
    public static class TestDbConfig
    {
        internal static void CreateTestDbContext<TSource>(this IServiceCollection service)
            where TSource : DbContext
        {
            var serviceProvider = service.ChangeTestDbContext<TSource>();

            // Create a scope to obtain a reference to the database
            using var scope = serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<TSource>();
            // Ensure the database is deleted.
            db.Database.EnsureDeleted();
            // Ensure the database is created.
            db.Database.EnsureCreated();
        }

        private static ServiceProvider ChangeTestDbContext<TSource>(this IServiceCollection services)
            where TSource : DbContext
        {
            // Remove the app's MovieDbContext registration.
            var descriptor =
                services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<TSource>));
            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            // Add MovieDbContext using an in-memory database for testing.
            services.AddDbContext<TSource>(options => { options.UseInMemoryDatabase(nameof(TSource)); });

            // Build the service provider.
            return services.BuildServiceProvider();
        }
    }
}