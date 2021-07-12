using System.Web.Http;
using Owin;
using WebApi_fx;

namespace WebApiTest_fx.CommonLib
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var configuration = new HttpConfiguration();
            TestDependencyInjectionConfig.Register(configuration);
            WebApiConfig.Register(configuration);

            app.UseWebApi(configuration);
        } 
    }
}