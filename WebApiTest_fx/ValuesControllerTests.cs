using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using WebApi_fx.Service;
using WebApiTest_fx.CommonLib;
using static WebApiTest_fx.CommonLib.TestDependencyInjectionConfig;

namespace WebApiTest_fx
{
    [TestFixture]
    public class ValuesControllerTests : IntegrationTestBase
    {
        [Test]
        public async Task Values_NoMock()
        {
            const string url = "api/values";
            var message = await base.CreateHttpClient().GetAsync(url);
            message.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await message.Content.ReadAsAsync<IEnumerable<string>>();
            content.Should().BeEquivalentTo("value1", "value2");
        }

        [Test]
        public async Task Values_Mock()
        {
            var service = Substitute.For<IValuesService>();
            service.GetValues().Returns(new[] {"value1", "value2", "value3"});
            
            const string url = "api/values";
            var message = await base.CreateHttpClient(() => Services.AddScoped(_ => service)).GetAsync(url);
            message.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await message.Content.ReadAsAsync<IEnumerable<string>>();
            content.Should().BeEquivalentTo("value1", "value2", "value3");
        }
    }
}