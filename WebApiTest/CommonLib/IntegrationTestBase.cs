using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using WebApi;
using WebApi.Database;
using WebApi.Database.Models;

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

        protected void MovieDbConfig()
        {
            _webHost = _webHost.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Remove the app's MovieDbContext registration.
                    var descriptor =
                        services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<MovieDbContext>));
                    if (descriptor is not null)
                    {
                        services.Remove(descriptor);
                    }

                    // Add MovieDbContext using an in-memory database for testing.
                    services.AddDbContext<MovieDbContext>(options => { options.UseInMemoryDatabase("MovieTest"); });

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

                    this.InitDb(db);
                });
            });
        }

        private void InitDb(MovieDbContext db)
        {
            db.Movie.AddRange(new List<Movie>()
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

        private static void DefaultConfigureServices(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(configurationBuilder =>
                configurationBuilder
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.IntegrationTest.json"));
            //.UseEnvironment("Test");
        }
    }
}