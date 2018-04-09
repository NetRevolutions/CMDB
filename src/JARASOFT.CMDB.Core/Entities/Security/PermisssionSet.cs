using System;
using System.Runtime.Serialization;
using JARASOFT.CMDB.Core.Entities.Common;

namespace JARASOFT.CMDB.Core.Entities.Security
{
    [Serializable]
    [DataContract(Namespace = "http://schemas.datacontract.org/2018/03/JARASOFT.Entites")]
    public partial class PermisssionSet
    {
        /// <summary>
        /// Represent the column PermissionSetID
        /// </summary>
        [DataMember]
        [AuditEntityKey]
        [Column("PermissionSetID")]
        public Guid PermissionSetID { get; set; }

        /// <summary>
        /// Represent the column InternalName
        /// </summary>
        [DataMember]
        [Column("InternalName")]
        public string InternalName { get; set; }

        /// <summary>
        /// Represent the column IsActive
        /// </summary>
        [DataMember]
        [Column("IsActive")]
        public bool IsActive { get; set; }

        /// <summary>
        /// Represent the column ApplicationID
        /// </summary>
        [DataMember]
        [Column("ApplicationID")]
        public Guid ApplicationID { get; set; }

        /// <summary>
        /// Represent the column LastModified
        /// </summary>
        [DataMember]
        [Column("LastModified")]
        public DateTime? LastModified { get; set; }

        /// <summary>
        /// Represent the column ModifiedBy
        /// </summary>
        [DataMember]
        [Column("ModifiedBy")]
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Represent the column CreationDate
        /// </summary>
        [DataMember]
        [Column("CreationDate")]
        public DateTime? CreationDate { get; set; }

        /// <summary>
        /// Represent the column CreatedBy
        /// </summary>
        [DataMember]
        [Column("CreatedBy")]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Represent the column IsSystem
        /// </summary>
        [DataMember]
        [Column("IsSystem")]
        public bool IsSystem { get; set; }

        /// <summary>
        /// Represent the column ApplicationName
        /// </summary>
        [DataMember]
        [Column("ApplicationName")]
        public string ApplicationName { get; set; }

        /// <summary>
        /// Represent the column GroupName
        /// </summary>
        [DataMember]
        [Column("GroupName")]
        public string GroupName { get; set; }

        /// <summary>
        /// Represent the column PermissionType
        /// </summary>
        [DataMember]
        [Column("PermissionType")]
        public int PermissionType { get; set; }

        /// <summary>
        /// Represent the column PermissionSetIdentity
        /// </summary>
        [DataMember]
        [Column("PermissionSetIdentity")]
        public Int64 PermissionSetIdentity { get; set; }
    }
}
