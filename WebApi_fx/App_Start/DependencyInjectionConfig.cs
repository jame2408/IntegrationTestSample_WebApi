using System.Web.Http;
using Microsoft.Extensions.DependencyInjection;
using WebApi_fx.Service;

namespace WebApi_fx
{
    public class DependencyInjectionConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var services = ConfigureServices(new ServiceCollection());

            var provider = services.BuildServiceProvider();

            var resolver = new DefaultDependencyResolver(provider);
            config.DependencyResolver = resolver;
        }

        /// <summary>
        ///     使用 MS DI 註冊
        /// </summary>
        /// <returns></returns>
        public static ServiceCollection ConfigureServices(ServiceCollection services)
        {
            //使用 Microsoft.Extensions.DependencyInjection 註冊
            services.AddControllersAsServices(typeof(DependencyInjectionConfig).Assembly.GetExportedTypes());

            services.AddScoped<IValuesService, ValuesService>();
            
            return services;
        }
    }
}