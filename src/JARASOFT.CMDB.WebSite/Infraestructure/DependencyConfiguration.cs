using Microsoft.Practices.Unity;
using JARASOFT.CMDB.Services.Repositories;
using JARASOFT.CMDB.Services.Repositories.Infraestructure;
//using JARASOFT.CMDB.Services.Services;
using JARASOFT.CMDB.Services.SqlServer;

namespace JARASOFT.CMDB.WebSite.Infraestructure
{
    public static class DependencyConfiguration
    {
        public static void Configure(IUnityContainer container)
        {
            // Register your dependencies
            container.RegisterType<IDataAccessBridge, DataAccessBridge>();

            // Services

            // Repositories


        }
    }
}