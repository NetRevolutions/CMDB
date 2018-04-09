using System;
using System.Runtime.Serialization;
using JARASOFT.CMDB.Core.Entities.Common;

namespace JARASOFT.CMDB.Core.Entities.Security
{
    public partial class UserGroupFlatView
    {
        /// <summary>
        /// Represent the Column UserGroupID
        /// </summary>
        [DataMember]
        [AuditEntityKey]
        [Column("UserGroupID")]
        public Guid UserGroupID { get; set; }

        /// <summary>
        /// Represent the Column ApplicationID
        /// </summary>
        [DataMember]
        [AuditEntityKey]
        [Column("ApplicationID")]
        public Guid ApplicationID { get; set; }

        /// <summary>
        /// Represent the Column AppGroupID
        /// </summary>
        [DataMember]
        [AuditEntityKey]
        [Column("AppGroupID")]
        public Guid AppGroupID { get; set; }

        /// <summary>
        /// Represent the Column InternalName
        /// </summary>
        [DataMember]
        [AuditEntityKey]
        [Column("InternalName")]
        public string InternalName { get; set; }

        /// <summary>
        /// Represent the Column Sid
        /// </summary>
        [DataMember]
        [Column("SID")]
        public string Sid { get; set; }

        /// <summary>
        /// Represent the Column IsActive
        /// </summary>
        [DataMember]
        [Column("IsActive")]
        public bool IsActive { get; set; }

        /// <summary>
        /// Represent the Column LastModified
        /// </summary>
        [DataMember]
        [Column("LastModified")]
        public DateTime? LastModified { get; set; }

        /// <summary>
        /// Represent the Column ModifiedBy
        /// </summary>
        [DataMember]
        [Column("ModifiedBy")]
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Represent the Column CreationDate
        /// </summary>
        [DataMember]
        [Column("CreationDate")]
        public DateTime? CreationDate { get; set; }

        /// <summary>
        /// Represent the Column IsDeleted
        /// </summary>
        [DataMember]
        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Represent the Column CreatedBy
        /// </summary>
        [DataMember]
        [Column("CreatedBy")]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Represent the Column IsGroupManager
        /// </summary>
        [DataMember]
        [Column("IsGroupManager")]
        public bool IsGroupManager { get; set; }

        /// <summary>
        /// Represent the Column IsGroupSuperUser
        /// </summary>
        [DataMember]
        [Column("IsGroupSuperUser")]
        public bool IsGroupSuperUser { get; set; }
    }
}
