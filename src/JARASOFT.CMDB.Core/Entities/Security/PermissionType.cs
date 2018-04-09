using System;
using System.Runtime.Serialization;

namespace JARASOFT.CMDB.Core.Entities.Security
{
    [Serializable]
    [DataContract(Namespace = "http://schemas.datacontract.org/2018/03/JARASOFT.Entites")]
    public enum PermissionType
    {
        [EnumMember]
        None = 1,
        [EnumMember]
        View = 2,
        [EnumMember]
        Edit = 3,
        [EnumMember]
        Run = 4,
    }
}
