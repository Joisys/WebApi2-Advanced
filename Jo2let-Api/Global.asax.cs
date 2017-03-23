using System.Web.Http;

namespace Jo2let.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        //TODO: could remove this file as becase the OWIN Startup class
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            UnityConfig.RegisterComponents();
        }
    }
}
