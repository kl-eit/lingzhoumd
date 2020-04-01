using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ling.Domains.Abstract;
using Ling.Domains.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ling.Dashboard.Session;
using Ling.Common;
using Ling.Domains.ResponseObject;
using Ling.Domains.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using static Ling.Common.Constants;

namespace Ling.Dashboard.Controllers
{
    public class AccountController : Controller
    {
        #region Declaration
        IUserRepository _userRepository;
        string encryptedPassword = string.Empty;

        #endregion

        #region Constructor

        public AccountController()
        {
            _userRepository = new UserRepository();

        }

        #endregion

        #region Actions
        // GET: Account
        /// <summary>
        /// Login
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login(String returnurl = "")
        {
            //if (Request.IsAuthenticated)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            ViewBag.ReturnURL = returnurl;
            return View();
        }


        /// <summary>
        /// Login 
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string username, string password, string returnurl = "")
        {
            string encryptedPassword = Security.Hash(password);
            string Check = Request.Form["RememberMe"];
            if (Check == "on")
            {
                CookieOptions userOption = new CookieOptions();
                CookieOptions passwordOption = new CookieOptions();

                userOption.Expires = DateTime.Now.AddDays(30);
                passwordOption.Expires = DateTime.Now.AddDays(30);

                Response.Cookies.Append(Constants.REMEMBERMEUSERNAME, username, userOption);
                Response.Cookies.Append(Constants.REMEMBERMEPASSWORD, password, userOption);

                if (Request.Cookies[Constants.REMEMBERMEUSERNAME] != null && Request.Cookies[Constants.REMEMBERMEPASSWORD] != null)
                {
                    Response.Cookies.Append(Constants.REMEMBERMEUSERNAME, username, userOption);
                    Response.Cookies.Append(Constants.REMEMBERMEPASSWORD, password, userOption);
                }

            }
            else
            {
                CookieOptions userOption = new CookieOptions();
                CookieOptions passwordOption = new CookieOptions();

                userOption.Expires = DateTime.Now.AddDays(30);
                passwordOption.Expires = DateTime.Now.AddDays(30);

                Response.Cookies.Append(Constants.REMEMBERMEUSERNAME, username, userOption);
                Response.Cookies.Append(Constants.REMEMBERMEPASSWORD, password, userOption);

                if (Request.Cookies[Constants.REMEMBERMEUSERNAME] != null && Request.Cookies[Constants.REMEMBERMEPASSWORD] != null)
                {
                    Response.Cookies.Append(Constants.REMEMBERMEUSERNAME, username, userOption);
                    Response.Cookies.Append(Constants.REMEMBERMEPASSWORD, password, userOption);
                }
            }


            string ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            ResponseObjectForAnything responseObjectForAnything = _userRepository.UserAuthentication(username, encryptedPassword);
            if (responseObjectForAnything.ResultCode == Constants.RESPONSE_SUCCESS)
            {
                UserLoginViewModel user = (UserLoginViewModel)responseObjectForAnything.ResultObject;
                if (user != null)
                {

                    if ((user.Role.ToUpper() == UserRoles.Admin.ToString()))
                    {
                        //// INITIALIZE FORMSAUTHENTICATION
                        //FormsAuthentication.Initialize();

                        //// CREATE A NEW TICKET USED FOR AUTHENTICATION                        
                        //FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, user.ID.ToString(), DateTime.Now, DateTime.Now.AddMinutes(60), false, user.Role);

                        //// ENCRYPT THE COOKIE USING THE MACHINE KEY FOR SECURE TRANSPORT
                        //string encTicket = FormsAuthentication.Encrypt(authTicket);

                        //// CREATE AND ADD THE COOKIES TO THE LIST FOR OUTGOING RESPONSE
                        //HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                        //Response.Cookies.Add(faCookie);

                        //UserSession.InitializeUserSession(user.ID, user);

                        if (string.IsNullOrEmpty(returnurl))
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            Response.Redirect(returnurl);
                        }
                    }
                }
            }

            ViewBag.ErrorMessage = "Invalid username or password!";

            return View();
        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            return View();
            //return RedirectToAction("Login", "Account");
        }

        public ActionResult MyProfile()
        {
            return View();
        }
        #endregion
    }
}