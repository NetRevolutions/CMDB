using System.Security.Principal;

namespace JARASOFT.CMDB.Core.Entities.Security
{
    public class JarasoftIdentity : GenericIdentity
    {
        public JarasoftIdentity(string name)
            : base(name)
        { }

        public JarasoftIdentity(string name, string type)
            : base(name, type)
        { }
    }
}
