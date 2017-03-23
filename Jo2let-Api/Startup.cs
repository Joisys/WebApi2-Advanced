using System.Web.Http;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Jo2let.Api.Startup))]
namespace Jo2let.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            //Http Configuration
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);

            app.UseWebApi(config);

            //Authentication Configuration
            ConfigureAuth(app);

        }
    }
}