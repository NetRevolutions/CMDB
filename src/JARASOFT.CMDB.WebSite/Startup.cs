using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.Unity;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Web.Mvc;
using JARASOFT.CMDB.WebSite;
using JARASOFT.CMDB.WebSite.Infraestructure;

[assembly: OwinStartup(typeof(JARASOFT.CMDB.WebSite.Startup))]

namespace JARASOFT.CMDB.WebSite
{
    public class Startup
    {
        private readonly IUnityContainer Container = new UnityContainer();
        public void Configuration(IAppBuilder app)
        {
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory());
            //Logger.SetLogWriter((new LogWriterFactory()).Create());
            //ExceptionPolicyFactory Exception_factory = new ExceptionPolicyFactory(ConfigurationSourceFactory.Create());
            //ExceptionPolicy.SetExceptionManager(Exception_factory.CreateManager());

            DependencyConfiguration.Configure(this.Container);

            IDependencyResolver resolver = DependencyResolver.Current;

            IDependencyResolver newResolver = new UnityDependencyResolver(this.Container, resolver);

            this.Container.RegisterType<IViewPageActivator, CustomViewPageActivator>(new InjectionConstructor(this.Container));

            DependencyResolver.SetResolver(newResolver);



            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
