using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JARASOFT.CMDB.Core.Entities.Security;
using JARASOFT.CMDB.WebSite.Infraestructure;

namespace JARASOFT.CMDB.WebSite.Services
{
    public class CMDBCacheNames
    {
        public readonly static string JR_PRINCIPAL_NAME = "CMDBPrincipal";
    }

    public class CMDBCacheService: ICMDBCacheService
    {
        public JarasoftPrincipal GetPrincipal(GuidToken token)
        {
            var profile = HttpContext.Current.Cache[CMDBCacheNames.JR_PRINCIPAL_NAME];
            if (profile != null)
            {
                return (JarasoftPrincipal)profile;
            }
            return null;
        }

        public void SavePrincipal(JarasoftPrincipal data, GuidToken token)
        {
            HttpContext.Current.Cache.Insert(CMDBCacheNames.JR_PRINCIPAL_NAME, data);
        }

        // TODO: Pending copy
    }
}