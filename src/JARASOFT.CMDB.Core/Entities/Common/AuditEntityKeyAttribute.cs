using System;

namespace JARASOFT.CMDB.Core.Entities.Common
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AuditEntityKeyAttribute: System.Attribute
    {        public AuditEntityKeyAttribute()
        { }
    }
}
