using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARASOFT.CMDB.WebSite.Infraestructure
{
    public class CustomHandlerData : ExceptionHandlerData
    {
        public CustomHandlerData()
            : base(typeof(CustomExceptionHandler))
        {

        }

        public override IExceptionHandler BuildExceptionHandler()
        {
            return new CustomExceptionHandler(this.logCategory);
        }

        [ConfigurationProperty("logCategory", IsRequired = false)]
        public String logCategory
        {
            get { return (String)this["logCategory"]; }
            set { this["logCategory"] = value; }
        }
    }
}