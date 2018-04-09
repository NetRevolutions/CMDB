using System;
using NUnit.Framework;

namespace JARASOFT.CMDB.Services.FunctionalTest.Infraestructure
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ReleaseAttribute : CategoryAttribute
    {
        public ReleaseAttribute(string Release)
            : base(string.Format("r_{0}", Release))
        { }
    }
}
