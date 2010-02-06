using StructureMap;
using StructureMap.Configuration.DSL;
using NHibernate;
using Kona;
using Kona.App.Services;
using Kona.Services;
using System.Web;

namespace Commerce.MVC.Web {
    public static class Bootstrapper {

        public static void ConfigureStructureMap() {
            StructureMapConfiguration.AddRegistry(new StoreRegistry());
        }
    }

    public class StoreRegistry : Registry {
        protected override void configure() {
             

            ForRequestedType<ISession>()
                .TheDefault.Is.ConstructedBy(x => MvcApplication.SessionFactory.GetCurrentSession());
            
            ForRequestedType<IStoreService>()
              .TheDefaultIsConcreteType<StoreService>();
            
            ForRequestedType<HttpContextBase>()
                .TheDefault.Is.ConstructedBy(x => new HttpContextWrapper(HttpContext.Current));
           
            ForRequestedType<ICustomerService>()
             .TheDefaultIsConcreteType<CustomerService>();
         
        }
    }
}
