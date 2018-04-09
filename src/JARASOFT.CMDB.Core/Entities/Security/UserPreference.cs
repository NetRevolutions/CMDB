using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using JARASOFT.CMDB.Core.Entities.Common;

namespace JARASOFT.CMDB.Core.Entities.Security
{
    [Serializable]
    [DataContract(Namespace = "http://schemas.datacontract.org/2018/03/JARASOFT.Entites")]
    [KnownType(typeof(UserPreferenceValue))]
    public partial class UserPreference
    {
        public UserPreference()
        {
            this.Values = new List<UserPreferenceValue>();
        }

        /// <summary>
        /// Represent the Column UserID
        /// </summary>
        [DataMember]
        [AuditEntityKey]
        [Column("UserID")]
        public string UserID { get; set; }

        /// <summary>
        /// Represent the Column ApplicationID
        /// </summary>
        [DataMember]
        [AuditEntityKey]
        [Column("ApplicationID")]
        public Guid ApplicationID { get; set; }

        /// <summary>
        /// Represent the Column UserOptionID
        /// </summary>
        [DataMember]
        [AuditEntityKey]
        [Column("UserOptionID")]
        public ObjectType UserOptionID { get; set; }

        /// <summary>
        /// Represent the Column AddToDashBoard
        /// </summary>
        [DataMember]
        [Column("AddToDashBoard")]
        public bool? AddToDashBoard { get; set; }

        /// <summary>
        /// Represent the Column Value
        /// </summary>
        [DataMember]
        [Column("Value")]
        public string Value { get; set; }

        /// <summary>
        /// Represent the Column ModifiedBy
        /// </summary>
        [DataMember]
        [Column("ModifiedBy")]
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Represent the Column LastModified
        /// </summary>
        [DataMember]
        [Column("LastModified")]
        public DateTime? LastModified { get; set; }

        public List<UserPreferenceValue> Values { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public string ApplicationName { get; set; }

        [DataMember]
        public string UserDateTimeZone { get; set; }

    }
}
