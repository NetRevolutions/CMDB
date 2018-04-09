using System.Runtime.Serialization;

namespace JARASOFT.CMDB.Core.Entities.Security
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2018/03/CMDB.Entities")]
    public enum ApplicationRegion
    {
        [EnumMember]
        US,

        [EnumMember]
        EU,

        [EnumMember]
        DR,

        [EnumMember]
        DEV
    }
}
