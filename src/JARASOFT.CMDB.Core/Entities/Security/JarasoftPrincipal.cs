using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Principal;

namespace JARASOFT.CMDB.Core.Entities.Security
{
    public class JarasoftPrincipal : GenericPrincipal
    {
        public JarasoftPrincipal(JarasoftIdentity identity, string[] roles)
            : base(identity, roles)
        {
            Domain = string.Empty;
            LoginName = string.Empty;
        }

        public bool IsSuperAdministrator()
        {
            return IsInRole("SysAdmin");
        }

        public long UserInstanceID { get; set; }
        public string LoginName { get; set; }
        public string Domain { get; set; }
        public string FullLoginName
        {
            get { return string.Format("{0}{1}{2}", Domain, "\\", LoginName); }
        }

        public string UserDisplayName { get; set; }
        public bool IsApplicationSupportUser { get; set; }

        public ICollection<PermisssionSet> Roles { get; set; }
        public ICollection<UserBusinessUnitFlatView> BusinessUnit { get; set; }

        public ICollection<UserPermissionSetItem> PermissionSet { get; set; }

        public ICollection<UserGroupFlatView> Groups { get; set; }

        public ApplicationRegion? Region { get; set; }
    }
}
