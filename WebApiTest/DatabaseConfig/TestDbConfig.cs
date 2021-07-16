using Microsoft.Extensions.DependencyInjection;
using WebApiTest.DatabaseConfig.Movie;

namespace WebApiTest.DatabaseConfig
{
    public static class TestDbConfig
    {
        internal static void CreateTestDbContextFactory(this IServiceCollection service, DatabaseEnum database)
        {
            if (database is DatabaseEnum.Movie)
            {
                new MovieTestDbContextConfig().Set(service);
            }
        }
    }
}