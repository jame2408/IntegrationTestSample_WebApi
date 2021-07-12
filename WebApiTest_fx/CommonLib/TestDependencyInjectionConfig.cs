using System;
using System.Web.Http;
using Microsoft.Extensions.DependencyInjection;
using WebApi_fx;

namespace WebApiTest_fx.CommonLib
{
    internal static class TestDependencyInjectionConfig
    {
        internal static readonly ServiceCollection Services = new();
        private static HttpConfiguration _httpConfig;

        /// <summary>
        /// OWIN Startup 取得正式機的 DI 註冊
        /// </summary>
        /// <param name="config"></param>
        internal static void Register(HttpConfiguration config)
        {
            _httpConfig = config;
            config.DependencyResolver = GetProductionRegister();
        }

        /// <summary>
        /// 取得正式機的 DI 註冊
        /// </summary>
        /// <returns></returns>
        internal static DefaultDependencyResolver GetProductionRegister()
        {
            DependencyInjectionConfig.ConfigureServices(Services);
            return Rebuild();
        }

        /// <summary>
        /// 依據傳入 serviceCollection 註冊
        /// </summary>
        /// <param name="serviceCollection"></param>
        internal static void RegisterResettableType(Func<IServiceCollection> serviceCollection)
        {
            serviceCollection.Invoke();
            Rebuild();
        }

        /// <summary>
        /// 重建 DI 註冊
        /// </summary>
        /// <returns></returns>
        private static DefaultDependencyResolver Rebuild()
        {
            var provider = Services.BuildServiceProvider();

            var resolver = new DefaultDependencyResolver(provider);
            _httpConfig.DependencyResolver = resolver;
            
            return resolver;
        }
    }
}