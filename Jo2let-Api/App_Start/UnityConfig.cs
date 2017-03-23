using System.Web;
using Microsoft.Practices.Unity;
using Jo2let.Data;
using Jo2let.Interface.Interface;
using Jo2let.Interface.Repository;
using Jo2let.Service;
using Jo2let.Infrastructure;
using Jo2let.Infrastructure.Repository;
using Jo2let.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace Jo2let.Api
{
    public static class UnityConfig
    {
        public static UnityContainer RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IDatabaseFactory, DatabaseFactory>(new HierarchicalLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());

            container.RegisterType<ILocationService, LocationService>();
            container.RegisterType<IPropertyService, PropertyService>();

            container.RegisterType<IPropertyRepository, PropertyRepository>();
            container.RegisterType<ILocationRepository, LocationRepository>();

            container.RegisterType<IAuthenticationManager>(new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new InjectionConstructor(typeof(PropertyDbContext)));

            return container;
        }

    }
}