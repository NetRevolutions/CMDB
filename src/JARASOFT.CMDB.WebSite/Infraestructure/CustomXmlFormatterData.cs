using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;

namespace JARASOFT.CMDB.WebSite.Infraestructure
{
    public class CustomXmlFormatterData : FormatterData
    {
        public override ILogFormatter BuildFormatter()
        {
            return new CustomXmlFormatter();
        }
    }
}