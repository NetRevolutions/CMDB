using System;
using System.Globalization;
using JARASOFT.CMDB.Core.Entities;
using JARASOFT.CMDB.Core.Entities.Security;

namespace JARASOFT.CMDB.WebSite.Services
{
    public interface ICMDBSessionService
    {
        UserInfo UserInfo { get; set; }
        JarasoftPrincipal Principal { get; set; }
        CultureInfo CultureInfo { get; set; }
        //ProfileEntity Profile { get; set; }
    }
}
