using Ling.Common;
using Ling.Domains.Abstract;
using Ling.Domains.Concrete;
using Ling.Domains.ResponseObject;
using Ling.Domains.ViewModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ling.Dashboard.Session
{
    public class UserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        IUserRepository _userRepository;

        public UserSession(IHttpContextAccessor httpContextAccessor, IConfiguration iConfiguration)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
            _userRepository = new UserRepository(iConfiguration);
        }

        public int LoginUserID
        {
            get
            {
                int retVal = 0;
                if (_session.GetString(Constants.USERSESSION_USERID) != null)
                {
                    retVal = Convert.ToInt32(_session.GetString(Constants.USERSESSION_USERID));
                }
                else
                {
                    int authUserID = GetAuthCookieUserID();
                    if (authUserID > 0)
                    {
                        InitializeUserSession(authUserID);
                        retVal = Convert.ToInt32(_session.GetString(Constants.USERSESSION_USERID));
                    }
                }
                return retVal;
            }
            set
            {
                _session.SetString(Constants.USERSESSION_USERID, value.ToString());
            }
        }

        public string LoginUserName
        {
            get
            {
                string retVal = string.Empty;
                if (_session.GetString(Constants.USERSESSION_USERNAME) != null)
                {
                    retVal = Convert.ToString(_session.GetString(Constants.USERSESSION_USERNAME));
                }
                else
                {
                    int authUserID = GetAuthCookieUserID();
                    if (authUserID > 0)
                    {
                        InitializeUserSession(authUserID);
                        retVal = Convert.ToString(_session.GetString(Constants.USERSESSION_USERNAME));
                    }
                }

                return retVal;
            }
            set
            {
                _session.SetString(Constants.USERSESSION_USERNAME,value.ToString());
            }
        }

        public int GetAuthCookieUserID()
        {
            int id = 0;

            var principal = _httpContextAccessor.HttpContext.User as ClaimsPrincipal;
            var userId = principal?.Claims
              .First(c => c.Type == ClaimTypes.Sid);

            id = Convert.ToInt32(userId.Value);
            //var claims = System.Security.Claims.ClaimsPrincipal.Current.Identities.First().Claims.ToList();
            //string val = claims?.FirstOrDefault(x => x.Type.Equals("", StringComparison.OrdinalIgnoreCase))?.Value;

            return id;
        }

        public void InitializeUserSession(int userID, UserLoginViewModel userModel = null)
        {
            if (userModel == null)
            {
                ResponseObjectForAnything responseObjectForAnything = _userRepository.ReInitUserSession(userID);
                if (responseObjectForAnything.ResultCode == Constants.RESPONSE_SUCCESS)
                {
                    userModel = (UserLoginViewModel)responseObjectForAnything.ResultObject;
                }
            }

            if (userModel != null)
            {
                LoginUserID = userModel.ID;
            }
        }
    }

    public static class HttpRequestExtensions
    {
        private const string RequestedWithHeader = "X-Requested-With";
        private const string XmlHttpRequest = "XMLHttpRequest"; public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }
            if (request.Headers != null)
            {
                return request.Headers[RequestedWithHeader] == XmlHttpRequest;
            }
            return false;
        }
    }
    public class AjaxOnlyAttribute : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
        {
            return routeContext.HttpContext.Request.IsAjaxRequest();
        }
    }
}
