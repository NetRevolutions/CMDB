using System;
using System.Configuration;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Security.Principal;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using JARASOFT.CMDB.Core.Entities.Security;
using JARASOFT.CMDB.WebSite.Services;

namespace JARASOFT.CMDB.WebSite.Infraestructure
{
    public class CMDBApplication : HttpApplication
    {
        public CMDBApplication()
        {
            this.Error += new EventHandler(CMDBApplication_Error);
            this.AuthenticateRequest += new EventHandler(CMDBApplication_AuthenticateRequest);
            this.AuthorizeRequest += new EventHandler(CMDBApplication_AuthorizeRequest);
            this.BeginRequest += new EventHandler(CMDBApplication_BeginRequest);
            this.EndRequest += new EventHandler(CMDBApplication_EndRequest);
            this.PreRequestHandlerExecute += new EventHandler(CMDBApplication_PreRequestHandlerExecute);
            this.PostAcquireRequestState += new EventHandler(CMDBApplication_PostAcquireRequestState);
        }

        private void CMDBApplication_EndRequest(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CMDBApplication_PostAcquireRequestState(object sender, EventArgs e)
        {
            if (HttpContext.Current != null
                && HttpContext.Current.User != null
                && HttpContext.Current.User.Identity != null
                && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string userId;
                string applicationId;
                ApplicationRegion? region;

                applicationId = ConfigurationManager.AppSettings["ApplicationID"];
#if DEBUG
                var token = new GuidToken(Guid.NewGuid());
                userId = HttpContext.Current.User.Identity.Name;
                region = ApplicationRegion.US;
#else
                var token = this.GetToken(out userId, out applicationId, out region);
#endif
                ICMDBSessionService CMDBSessionService = new CMDBSessionService();
                JarasoftPrincipal jarasoftPrincipal;
                UserInfo userInfo;
                CultureInfo culture;
                if (CMDBSessionService.Principal == null ||
                    CMDBSessionService.Principal.Identity.Name != userId)
                {
                    var userContext = new UserContextBridge();
                    userInfo = userContext.GetUserInfoFromService(userId, applicationId);
                    jarasoftPrincipal = userContext.GetPrincipal(userId, applicationId, region, userInfo);
                    culture = userContext.GetCultureInfo(userId, applicationId, region, userInfo);
                    CMDBSessionService.Principal = jarasoftPrincipal;
                    CMDBSessionService.UserInfo = userInfo;
                    CMDBSessionService.CultureInfo = culture;

                    //SicotycSessionService.Profile = userContext.GetProfileFromService(userInfo.SID);

                }
                else
                {
                    jarasoftPrincipal = CMDBSessionService.Principal;
                    culture = CMDBSessionService.CultureInfo;
                }
                HttpContext.Current.User = jarasoftPrincipal;
                Thread.CurrentPrincipal = jarasoftPrincipal;
                Thread.CurrentThread.CurrentCulture = culture;
            }
        }

        private void CMDBApplication_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CMDBApplication_BeginRequest(object sender, EventArgs e)
        {
            // Do nothing because it is just for debug mode
            #region
#if DEBUG
            var req = HttpContext.Current.Request;
            var res = HttpContext.Current.Response;
            if (req.Headers.Get("Origin") != null)
            {
                res.AppendHeader("Access-Control-Allow-Origin", "*");
            }
            res.AppendHeader("Access-Control-Allow-Credentials", "true");
            res.AppendHeader("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
            res.AppendHeader("Access-Control-Allow-Methods", "POST,GET,PUT,PATCH,DELETE,OPTIONS");
            if (req.HttpMethod == "OPTIONS")
            {
                res.StatusCode = 200;
                res.End();
            }
#endif
            #endregion
        }

        private void CMDBApplication_AuthorizeRequest(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CMDBApplication_AuthenticateRequest(object sender, EventArgs e)
        {
#if DEBUG
            var sid = Application["SID"] ?? string.Empty;
            var userPrincipal = new GenericPrincipal(new GenericIdentity(sid.ToString()), new string[] { string.Empty });
            System.Web.HttpContext.Current.User = userPrincipal;
            System.Threading.Thread.CurrentPrincipal = userPrincipal;
#endif
            // AuthenticateRequest
        }

        private void CMDBApplication_Error(object sender, EventArgs e)
        {
            var ex = HttpContext.Current.Server.GetLastError();
            if (!(ex is ThreadAbortException))
            {
                ExceptionPolicy.HandleException(ex, "General");
            }
        }

        private GuidToken GetToken(out string userId, out string applicationId, out ApplicationRegion? region)
        {
            userId = null;
            applicationId = null;
            region = null;
            GuidToken token = null;
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            string[] userData = authTicket.UserData.Split(new string[] { "||" }, StringSplitOptions.None);
            var tokenId = userData[0];
            userId = userData[1];
            applicationId = ConfigurationManager.AppSettings["ApplicationID"];
            region = ApplicationRegion.US;
            token = new GuidToken(Guid.Parse(tokenId));
            return token;
        }
    }
}