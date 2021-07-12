using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin.Hosting;
using NUnit.Framework;
using static WebApiTest_fx.CommonLib.TestDependencyInjectionConfig;

namespace WebApiTest_fx.CommonLib
{
    public class IntegrationTestBase
    {
        private const string HostAddress = "https://localhost:44334/";
        private readonly HttpClient _client = new() {BaseAddress = new Uri(HostAddress)};
        private IDisposable _webApp;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _webApp = WebApp.Start<Startup>(HostAddress);
        }

        protected HttpClient CreateHttpClient(Func<IServiceCollection> serviceCollection = null)
        {
            if (serviceCollection is null)
            {
                GetProductionRegister();
                return _client;
            }

            RegisterResettableType(serviceCollection);
            return _client;
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _webApp.Dispose();
        }
    }
}