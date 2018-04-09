using System;
using System.Globalization;
using System.Web;
using JARASOFT.CMDB.Core.Entities.Security;


namespace JARASOFT.CMDB.WebSite.Services
{
    public class CMDBSessionService : ICMDBSessionService
    {
        private static readonly string PrincipalSessionName = "JRPRINCIPAL";
        private static readonly string UserInfoSessionName = "JRUSERINFO";
        private static readonly string CultureInfoName = "JRCulture";
        private static readonly string ProfileSessionName = "JRPROFILE";

        public UserInfo UserInfo
        {
            get { return (UserInfo)HttpContext.Current.Session[UserInfoSessionName]; }
            set { HttpContext.Current.Session[UserInfoSessionName] = value; }
        }
        public JarasoftPrincipal Principal
        {
            get { return (JarasoftPrincipal)HttpContext.Current.Session[PrincipalSessionName]; }
            set { HttpContext.Current.Session[PrincipalSessionName] = value; }
        }
        public CultureInfo CultureInfo
        {
            get { return (CultureInfo)HttpContext.Current.Session[CultureInfoName]; }
            set { HttpContext.Current.Session[CultureInfoName] = value; }
        }

        /*
        public ProfileEntity Profile
        {
            get { return (ProfileEntity)HttpContext.Current.Session[ProfileSessionName]; }
            set { HttpContext.Current.Session[ProfileSessionName] = value; }
        }
        */
    }
}