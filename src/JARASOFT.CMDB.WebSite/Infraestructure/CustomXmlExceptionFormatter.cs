using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace JARASOFT.CMDB.WebSite.Infraestructure
{
    public class CustomXmlExceptionFormatter : XmlExceptionFormatter
    {
        public CustomXmlExceptionFormatter(TextWriter writer, Exception exception, Guid handlingInstanceId) :
            base(writer, exception, handlingInstanceId)
        {
        }

        public CustomXmlExceptionFormatter(XmlWriter xmlWriter, Exception exception, Guid handlingInstanceId) :
            base(xmlWriter, exception, handlingInstanceId)
        {
        }

        protected override void WriteFieldInfo(FieldInfo fieldInfo, object value)
        {
            if (value != null)
            {
                base.WriteFieldInfo(fieldInfo, value);
            }
        }
    }
}