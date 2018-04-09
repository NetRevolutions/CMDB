using System;

namespace JARASOFT.CMDB.Core.Entities.Common
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ColumnAttribute: Attribute
    {
        public string Name { get; set; }
        public ColumnAttribute(string name)
        {
            this.Name = name;
        }
    }
}
