using System;
using NUnit.Framework;

namespace JARASOFT.CMDB.Services.FunctionalTest.Infraestructure
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class JiraIdAttribute : CategoryAttribute
    {
        public JiraIdAttribute(string JiraId)
            : base(string.Format("s_{0}", JiraId))
        { }
    }
}
