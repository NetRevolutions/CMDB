using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using JARASOFT.CMDB.Core.Entities.Common;

namespace JARASOFT.CMDB.Core.Entities.Security
{
    [Serializable]
    [DataContract(Namespace = "http://schemas.datacontract.org/2018/03/JARASOFT.Entites")]
    public partial class UserInfo
    {
        [DataMember]
        [Column("InstanceID")]
        public long InstanceID { get; set; }

        [DataMember]
        [Column("CustomerID")]
        public long CustomerID { get; set; }

        [DataMember]
        [Column("IsMigrated")]
        public bool IsMigrated { get; set; }

        [DataMember]
        [Column("ADUserID")]
        public int ADUserID { get; set; }

        [DataMember]
        [Column("SID")]
        public string SID { get; set; }

        [DataMember]
        [Column("DomainName")]
        public string DomainName { get; set; }

        [DataMember]
        [Column("DomainShortName")]
        public string DomainShortName { get; set; }

        [DataMember]
        [Column("ApplicationID")]
        public string ApplicationID { get; set; }

        [DataMember]
        [Column("Prefix")]
        public string Prefix { get; set; }

        [DataMember]
        [Column("FirstName")]
        public string FirstName { get; set; }

        [DataMember]
        [Column("LastName")]
        public string LastName { get; set; }

        [DataMember]
        [Column("Title")]
        public string Title { get; set; }

        [DataMember]
        [Column("Company")]
        public string Company { get; set; }

        [DataMember]
        [Column("State")]
        public string State { get; set; }

        [DataMember]
        [Column("City")]
        public string City { get; set; }

        [DataMember]
        [Column("Country")]
        public string Country { get; set; }

        [DataMember]
        [Column("PostalCode")]
        public string PostalCode { get; set; }

        [DataMember]
        [Column("TelephoneNumber")]
        public string TelephoneNumber { get; set; }

        [DataMember]
        [Column("Mail")]
        public string Mail { get; set; }

        [DataMember]
        [Column("ContactName")]
        public string ContactName { get; set; }

        [DataMember]
        [Column("UserName")]
        public string UserName { get; set; }

        [DataMember]
        public string TimeZone { get; set; }

        [DataMember]
        public ICollection<UserGroupFlatView> Groups { get; set; }

        [DataMember]
        public ICollection<PermisssionSet> Roles { get; set; }

        [DataMember]
        public ICollection<UserBusinessUnitFlatView> BusinessUnits { get; set; }

        [DataMember]
        public ICollection<UserPreference> UserPreference { get; set; }

        [DataMember]
        public bool IsApplicationSupportUser { get; set; }

        [DataMember]
        public int? OverrideSessionTimeOut { get; set; }

    }
}
