using System;
using System.Runtime.Serialization;
using JARASOFT.CMDB.Core.Entities.Common;

namespace JARASOFT.CMDB.Core.Entities.Security
{
    [Serializable]
    [DataContract(Namespace = "http://schemas.datacontract.org/2018/03/JARASOFT.Entites")]
    public partial class UserPermissionSetItem
    {
        /// <summary>
        /// Represent the column ObjectID
        /// </summary>
        [DataMember]
        [Column("ObjectID")]
        public string ObjectID { get; set; }

        /// <summary>
        /// Represent the column ObjectType
        /// </summary>
        [DataMember]
        [Column("ObjectType")]
        public ObjectType ObjectType { get; set; }

        /// <summary>
        /// Represent the column PermissionType
        /// </summary>
        [DataMember]
        [Column("PermissionType")]
        public PermissionType PermissionType { get; set; }

        /// <summary>
        /// Represent the column Sequence
        /// </summary>
        [DataMember]
        [Column("Sequence")]
        public int Sequence { get; set; }

        /// <summary>
        /// Represent the column ApplicationID
        /// </summary>
        [DataMember]
        [Column("ApplicationID")]
        public Guid ApplicationID { get; set; }
    }
}
