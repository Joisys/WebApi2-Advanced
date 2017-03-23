using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Jo2let.Api.Mapper;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Unity.WebApi;

namespace Jo2let.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();

            //Register and Resolve Unity Container (It's important to register container in the same HttpConfiguration instance)
            var container = UnityConfig.RegisterComponents();
            config.DependencyResolver = new UnityDependencyResolver(container);

            //Define Formatter
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Web API configuration and services

            //Note: comment following code for not to authenticate ==============NOTE====================
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            config.Filters.Add(new AuthorizeAttribute());
            //===================================================================NOTE=====================

            //AutoMapper Configuration
            AutoMapperConfiguration.Configure();

            config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional }
                );
        }
    }
}
