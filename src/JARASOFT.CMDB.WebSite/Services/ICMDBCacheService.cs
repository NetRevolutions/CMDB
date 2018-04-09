using System;
using JARASOFT.CMDB.Core.Entities.Security;
using JARASOFT.CMDB.WebSite.Infraestructure;

namespace JARASOFT.CMDB.WebSite.Services
{
    public interface ICMDBCacheService
    {
        JarasoftPrincipal GetPrincipal(GuidToken token);

        void SavePrincipal(JarasoftPrincipal data, GuidToken token);

        // TODO: Pending copy
    }
}