using System;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using WebApi;

namespace WebApiTest.CommonLib
{
    public class IntegrationTestBase
    {
        private WebApplicationFactory<Startup> _factory;
        private WebApplicationFactory<Startup> _webHost;

        [OneTimeSetUp]
        public void OneTimeSetUpBase()
        {
            _factory = new WebApplicationFactory<Startup>();
        }

        [OneTimeTearDown]
        public void OneTimeTearDownBase()
        {
            _factory.Dispose();
        }

        [SetUp]
        public void SetUpBase()
        {
            _webHost = _factory.WithWebHostBuilder(DefaultConfigureServices);
        }

        protected HttpClient CreateHttpClient()
        {
            return _webHost.CreateClient();
        }

        protected void ConfigureServices(Action<IServiceCollection> configureService)
        {
            _webHost = _webHost.WithWebHostBuilder(builder =>
            {
                if (configureService is not null)
                {
                    builder.ConfigureTestServices(configureService);
                }
            });
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