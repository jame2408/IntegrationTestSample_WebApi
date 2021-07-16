using Microsoft.Extensions.DependencyInjection;

namespace WebApiTest.DatabaseConfig
{
    public interface ITestDbContextConfig
    {
        void Set(IServiceCollection services);
    }
}