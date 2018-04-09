using System;
using NUnit.Framework;

namespace JARASOFT.CMDB.Services.FunctionalTest.Infraestructure
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class OwnerAttribute : CategoryAttribute
    {
        public OwnerAttribute(string Email)
            : base(string.Format("o_{0}", Email))
        { }
    }
}
