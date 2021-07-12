using System;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using WebApi;

namespace WebApiTest.CommonLib
{
    public class IntegrationTestBase
    {
        private WebApplicationFactory<Startup> _factory;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _factory = new WebApplicationFactory<Startup>();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _factory.Dispose();
        }

        protected HttpClient CreateHttpClient()
        {
            var webHost = _factory.WithWebHostBuilder(DefaultConfigureServices);
            return webHost.CreateClient();
        }

        protected HttpClient CreateHttpClient(Action<IServiceCollection> configureService)
        {
            var webHost = _factory.WithWebHostBuilder(builder =>
            {
                DefaultConfigureServices(builder);
                if (configureService is not null)
                {
                    builder.ConfigureServices(configureService);
                }
            });
            return webHost.CreateClient();
        }

        private static void DefaultConfigureServices(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(configurationBuilder => 
                    configurationBuilder
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.IntegrationTest.json"))
                .UseEnvironment("Test");
        }
    }
}