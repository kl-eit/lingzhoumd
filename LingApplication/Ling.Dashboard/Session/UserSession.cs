using Ling.Common;
using Ling.Domains.Abstract;
using Ling.Domains.Concrete;
using Ling.Domains.ResponseObject;
using Ling.Domains.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ling.Dashboard.Session
{
    public class UserSession
    {
        //private static HttpContext _CurrentContext
        //{
        //    get
        //    {
        //        return HttpContext.Current;
        //    }
        //}

        //public static int LoginUserID
        //{
        //    get
        //    {
        //        int retVal = 0;
        //        if (_CurrentContext.Session[Constants.USERSESSION_USERID] != null)
        //        {
        //            retVal = Convert.ToInt32(_CurrentContext.Session[Constants.USERSESSION_USERID]);
        //        }
        //        else
        //        {
        //            int authUserID = GetAuthCookieUserID();
        //            if (authUserID > 0)
        //            {
        //                InitializeUserSession(authUserID);
        //                retVal = Convert.ToInt32(_CurrentContext.Session[Constants.USERSESSION_USERID]);
        //            }
        //        }
        //        return retVal;
        //    }
        //    set
        //    {
        //        _CurrentContext.Session[Constants.USERSESSION_USERID] = value;
        //    }
        //}

        //public static string LoginUserEmail
        //{
        //    get
        //    {
        //        string retVal = string.Empty;
        //        if (_CurrentContext.Session[Constants.USERSESSION_USEREMAIL] != null)
        //        {
        //            retVal = Convert.ToString(_CurrentContext.Session[Constants.USERSESSION_USEREMAIL]);
        //        }
        //        else
        //        {
        //            int authUserID = GetAuthCookieUserID();
        //            if (authUserID > 0)
        //            {
        //                InitializeUserSession(authUserID);
        //                retVal = Convert.ToString(_CurrentContext.Session[Constants.USERSESSION_USEREMAIL]);
        //            }
        //        }

        //        return retVal;
        //    }
        //    set
        //    {
        //        _CurrentContext.Session[Constants.USERSESSION_USEREMAIL] = value;
        //    }
        //}

        //public static string LoginUserFirstName
        //{
        //    get
        //    {
        //        string retVal = string.Empty;
        //        if (_CurrentContext.Session[Constants.USERSESSION_USERFIRSTNAME] != null)
        //        {
        //            retVal = Convert.ToString(_CurrentContext.Session[Constants.USERSESSION_USERFIRSTNAME]);
        //        }
        //        else
        //        {
        //            int authUserID = GetAuthCookieUserID();
        //            if (authUserID > 0)
        //            {
        //                InitializeUserSession(authUserID);
        //                retVal = Convert.ToString(_CurrentContext.Session[Constants.USERSESSION_USERFIRSTNAME]);
        //            }
        //        }

        //        return retVal;
        //    }
        //    set
        //    {
        //        _CurrentContext.Session[Constants.USERSESSION_USERFIRSTNAME] = value;
        //    }
        //}

        //public static string LoginUserLastName
        //{
        //    get
        //    {
        //        string retVal = string.Empty;
        //        if (_CurrentContext.Session[Constants.USERSESSION_USERLASTNAME] != null)
        //        {
        //            retVal = Convert.ToString(_CurrentContext.Session[Constants.USERSESSION_USERLASTNAME]);
        //        }
        //        else
        //        {
        //            int authUserID = GetAuthCookieUserID();
        //            if (authUserID > 0)
        //            {
        //                InitializeUserSession(authUserID);
        //                retVal = Convert.ToString(_CurrentContext.Session[Constants.USERSESSION_USERLASTNAME]);
        //            }
        //        }

        //        return retVal;
        //    }
        //    set
        //    {
        //        _CurrentContext.Session[Constants.USERSESSION_USERLASTNAME] = value;
        //    }
        //}

        //public static string LoginUserName
        //{
        //    get
        //    {
        //        string retVal = string.Empty;
        //        if (_CurrentContext.Session[Constants.USERSESSION_USERNAME] != null)
        //        {
        //            retVal = Convert.ToString(_CurrentContext.Session[Constants.USERSESSION_USERNAME]);
        //        }
        //        else
        //        {
        //            int authUserID = GetAuthCookieUserID();
        //            if (authUserID > 0)
        //            {
        //                InitializeUserSession(authUserID);
        //                retVal = Convert.ToString(_CurrentContext.Session[Constants.USERSESSION_USERNAME]);
        //            }
        //        }

        //        return retVal;
        //    }
        //    set
        //    {
        //        _CurrentContext.Session[Constants.USERSESSION_USERNAME] = value;
        //    }
        //}

        //public static string LoginUserAvatar
        //{
        //    get
        //    {
        //        string retVal = string.Empty;
        //        if (_CurrentContext.Session[Constants.USERSESSION_USERAVATAR] != null)
        //        {
        //            retVal = Convert.ToString(_CurrentContext.Session[Constants.USERSESSION_USERAVATAR]);
        //        }
        //        else
        //        {
        //            int authUserID = GetAuthCookieUserID();
        //            if (authUserID > 0)
        //            {
        //                InitializeUserSession(authUserID);
        //                retVal = Convert.ToString(_CurrentContext.Session[Constants.USERSESSION_USERAVATAR]);
        //            }
        //        }

        //        return retVal;
        //    }
        //    set
        //    {
        //        _CurrentContext.Session[Constants.USERSESSION_USERAVATAR] = value;
        //    }
        //}

        //public static string LoginUserRole
        //{
        //    get
        //    {
        //        string retVal = string.Empty;
        //        if (_CurrentContext.Session[Constants.USERSESSION_USERROLE] != null)
        //        {
        //            retVal = Convert.ToString(_CurrentContext.Session[Constants.USERSESSION_USERROLE]);
        //        }
        //        else
        //        {
        //            int authUserID = GetAuthCookieUserID();
        //            if (authUserID > 0)
        //            {
        //                InitializeUserSession(authUserID);
        //                retVal = Convert.ToString(_CurrentContext.Session[Constants.USERSESSION_USERROLE]);
        //            }
        //        }
        //        return retVal;
        //    }
        //    set
        //    {
        //        _CurrentContext.Session[Constants.USERSESSION_USERROLE] = value;
        //    }
        //}

        //public static int GetAuthCookieUserID()
        //{
        //    System.Security.Principal.IPrincipal principal = System.Threading.Thread.CurrentPrincipal;
        //    System.Security.Principal.IIdentity identity = principal == null ? null : principal.Identity;

        //    int id = 0;
        //    if (identity.IsAuthenticated)
        //    {
        //        id = identity == null ? 0 : Convert.ToInt32(identity.Name);
        //    }
        //    return id;
        //}

        //public static void InitializeUserSession(int userID, UserLoginViewModel userModel = null)
        //{
        //    if (userModel == null)
        //    {
        //        IUserRepository userRepository = new UserRepository();
        //        ResponseObjectForAnything responseObjectForAnything = userRepository.ReInitUserSession(userID);
        //        if (responseObjectForAnything.ResultCode == Constants.RESPONSE_SUCCESS)
        //        {
        //            userModel = (UserLoginViewModel)responseObjectForAnything.ResultObject;
        //        }
        //    }

        //    if (userModel != null)
        //    {
        //        LoginUserID = userModel.ID;
        //        LoginUserFirstName = userModel.FirstName;
        //        LoginUserLastName = userModel.LastName;
        //        LoginUserName = userModel.UserName;
        //        LoginUserEmail = userModel.Email;
        //        LoginUserAvatar = userModel.Avatar;
        //        LoginUserRole = userModel.Role;
        //    }
        //}

    }
}