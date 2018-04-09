using System;
using System.Runtime.Serialization;
using JARASOFT.CMDB.Core.Entities.Common;

namespace JARASOFT.CMDB.Core.Entities.Security
{
    [Serializable]
    [DataContract(Namespace = "http://schemas.datacontract.org/2018/03/JARASOFT.Entites")]
    public class UserPreferenceValue
    {
        [DataMember]
        [AuditEntityKey]
        [Column("UserID")]
        public string UserID { get; set; }

        [DataMember]
        [AuditEntityKey]
        [Column("ApplicationID")]
        public Guid ApplicationID { get; set; }

        [DataMember]
        [AuditEntityKey]
        [Column("UserOptionID")]
        public ObjectType UserOptionID { get; set; }

        [DataMember]
        [AuditEntityKey]
        [Column("Value")]
        public string Value { get; set; }
    }
}
