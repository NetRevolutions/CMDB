using System;
using System.Runtime.Serialization;
using JARASOFT.CMDB.Core.Entities.Common;

namespace JARASOFT.CMDB.Core.Entities.Security
{
    [Serializable]
    [DataContract(Namespace = "http://schemas.datacontract.org/2018/03/JARASOFT.Entites")]
    public partial class UserBusinessUnitFlatView
    {
        /// <summary>
        /// Represent the column Sid
        /// </summary>
        [DataMember]
        [Column("SID")]
        public string Sid { get; set; }

        /// <summary>
        /// Represent the column IsActive
        /// </summary>
        [DataMember]
        [Column("IsActive")]
        public bool IsActive { get; set; }

        /// <summary>
        /// Represent the column IsBusinessUnitManager
        /// </summary>
        [DataMember]
        [Column("IsBusinessUnitManager")]
        public bool IsBusinessUnitManager { get; set; }

        /// <summary>
        /// Represent the column BusinessUnitID
        /// </summary>
        [DataMember]
        [Column("BusinessUnitID")]
        public long BusinessUnitID { get; set; }

        /// <summary>
        /// Represent the column BusinessUnitName
        /// </summary>
        [DataMember]
        [Column("BusinessUnitName")]
        public string BusinessUnitName { get; set; }
    }
}
