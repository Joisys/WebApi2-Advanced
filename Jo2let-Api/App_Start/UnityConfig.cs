using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;
using Jo2let.Interface.Interface;
using Jo2let.Interface.Repository;
using Jo2let.Service;
using Jo2let.Infrastructure;
using Jo2let.Infrastructure.Repository;

namespace Jo2let.Api
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IDatabaseFactory, DatabaseFactory>();
            container.RegisterType<IUnitOfWork,UnitOfWork>();

            container.RegisterType<IPropertyRepository,PropertyRepository>();
            container.RegisterType<ILocationRepository,LocationRepository>();

            container.RegisterType<ILocationService, LocationService>();
            container.RegisterType<IPropertyService, PropertyService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}