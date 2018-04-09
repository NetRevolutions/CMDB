using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading;
using JARASOFT.CMDB.Core.Entities;
using JARASOFT.CMDB.Core.Entities.Security;
using JARASOFT.CMDB.Services.Repositories.Infraestructure;


namespace JARASOFT.CMDB.WebSite.Infraestructure
{
    public class UserContextBridge
    {
        public JarasoftPrincipal GetPrincipal(string userId, string applicationId, ApplicationRegion? region, UserInfo userInfo)
        {
            var roles = userInfo.Roles.ToList().ConvertAll<string>(r => r.InternalName).ToArray();
            var principal = new JarasoftPrincipal(new JarasoftIdentity(userInfo.SID), roles);
            principal.Groups = userInfo.Groups;
            principal.Roles = userInfo.Roles;
            principal.BusinessUnit = userInfo.BusinessUnits;
            principal.UserInstanceID = userInfo.InstanceID;
            principal.LoginName = userInfo.UserName;
            principal.Domain = userInfo.DomainShortName;
            principal.Region = region;
            principal.UserDisplayName = userInfo.ContactName;
            principal.IsApplicationSupportUser = userInfo.IsApplicationSupportUser;

            return principal;
        }

        public CultureInfo GetCultureInfo(string userId, string applicationId, ApplicationRegion? region, UserInfo userInfo)
        {
            var newCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            if (userInfo != null && userInfo.UserPreference != null)
            {
                var dateFormatPreference = userInfo.UserPreference.FirstOrDefault(up => up.UserOptionID == ObjectType.UserDateFormat && up.ApplicationID == new Guid(userInfo.ApplicationID));
                if (dateFormatPreference != null && !string.IsNullOrWhiteSpace(dateFormatPreference.Value))
                {
                    newCulture.DateTimeFormat.ShortDatePattern = dateFormatPreference.Value;
                    newCulture.DateTimeFormat.DateSeparator = dateFormatPreference.Value.Contains('/') ? "/" : (dateFormatPreference.Value.Contains('-') ? "-" : string.Empty);

                    var digitGroupingSymbol = userInfo.UserPreference.FirstOrDefault(item => item.ApplicationID == new Guid(userInfo.ApplicationID) && item.UserOptionID == ObjectType.DigitGroupingSymbol);
                    if (digitGroupingSymbol != null) newCulture.NumberFormat.NumberGroupSeparator = digitGroupingSymbol.Value ?? string.Empty;

                    var decimalSymbol = userInfo.UserPreference.FirstOrDefault(item => item.ApplicationID == new Guid(userInfo.ApplicationID) && item.UserOptionID == ObjectType.DecimalSymbol);
                    if (decimalSymbol != null) newCulture.NumberFormat.NumberDecimalSeparator = decimalSymbol.Value ?? string.Empty;
                }
            }

            return newCulture;
        }

        public UserInfo GetUserInfoFromService(string userId, string applicationId)
        {
            // TODO: Pending to implement a service to get those values
            return null;
        }

        // TODO: Pending copy
        /*
        internal ProfileEntity GetProfileFromService(string SID)
        {
            // TOD: Pending to implement a service to get those values
            //var profileService = new ProfileSe
            return null;

        }
        */
    }
}