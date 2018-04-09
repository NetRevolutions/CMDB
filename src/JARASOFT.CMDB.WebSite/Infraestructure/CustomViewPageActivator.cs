using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JARASOFT.CMDB.WebSite.Infraestructure
{
    public class CustomViewPageActivator : IViewPageActivator
    {
        private readonly IUnityContainer container;

        public CustomViewPageActivator(IUnityContainer container)
        {
            this.container = container;
        }

        public object Create(ControllerContext controllerContext, Type type)
        {
            return this.container.Resolve(type);
        }
    }
}