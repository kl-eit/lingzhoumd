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
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System.Threading;
using Microsoft.Extensions.Options;
using System.IO;
using Ling.Domains.Entities;

namespace Ling.Dashboard.Controllers
{
    public class AccountController : Controller
    {
        #region Declaration

        IUserRepository _userRepository;
        string encryptedPassword = string.Empty;
        UserSession _session;
        private AppSettings _appSettings { get; set; }
        #endregion

        #region Constructor

        public AccountController(IHttpContextAccessor httpContextAccessor, IConfiguration iConfiguration, IOptions<AppSettings> settings)
        {
            _userRepository = new UserRepository(iConfiguration);
            _session = new UserSession(httpContextAccessor, iConfiguration);
            _appSettings = settings.Value;
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

                    if ((user.Role == UserRoles.Admin.ToString()))
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



                        _session.InitializeUserSession(user.ID, user);

                        var claims = new[] { new Claim(ClaimTypes.Name, username), new Claim(ClaimTypes.Role, user.Role), new Claim(ClaimTypes.Sid, user.ID.ToString()) };

                        var identity = new ClaimsIdentity(claims, "User Identity");
                        var userPrincipal = new ClaimsPrincipal(new[] { identity });

                        Thread.CurrentPrincipal = userPrincipal;

                        HttpContext.SignInAsync(userPrincipal);

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
            HttpContext.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        public ActionResult MyProfile()
        {
            ResponseObjectForAnything responseObjectForAnything = _userRepository.SelectByID(_session.LoginUserID);
            if (responseObjectForAnything.ResultCode == Constants.RESPONSE_SUCCESS && responseObjectForAnything.ResultObject != null)
            {
                UserProfileViewModel model = (UserProfileViewModel)responseObjectForAnything.ResultObject;
                return View(model);
            }

            return View();
        }

        [HttpPost]
        public ActionResult MyProfile(Users model, [FromForm]IFormFile file)
        {
            string getExistFileName = string.Empty;
            string imageName = string.Empty;
            string sourceFilePath = string.Empty;

            model.ID = _session.LoginUserID;
            getExistFileName = Request.Form["hdfOldImageName"];
            if (file.Length > 0)
            {
                sourceFilePath = _appSettings.UploadFolderName + _appSettings.ProfileImagePath;
                //ServerSettings.UPLOADFOLDERNAME + ServerSettings.PROFILEIMAGEPATH;

                string fileName = file.FileName;
                if (!string.IsNullOrEmpty(fileName))
                {
                    imageName = Guid.NewGuid().ToString() + "_" + file.FileName;

                    string webURL = _appSettings.DashboardURL;

                    string fileDirectory = Path.Combine(_appSettings.DashboardPhysicalUploadPath, sourceFilePath);
                    if (!Directory.Exists(fileDirectory))
                    {
                        Directory.CreateDirectory(fileDirectory);
                    }

                    string fileSavePath = Path.Combine(_appSettings.DashboardPhysicalUploadPath, sourceFilePath, imageName);
                    using (var fileStream = new FileStream(fileSavePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    //string imageVersions = Constants.THUMBNAILIMAGERESIZER + "," + Constants.LARGEIMAGERESIZER + "," + Constants.SMALLIMAGERESIZER + "," + Constants.MEDIUMIMAGERESIZER;
                    //imageName = WebHelper.UploadFile(imageHttpFile, sourceFilePath, imageVersions, webURL);
                }
                else
                {
                    imageName = Request.Form["hdfImageName"];
                }
            }

            model.Avatar = imageName;
            model.UserName = _session.LoginUserName;
            model.ModifiedBy = _session.LoginUserID.ToString();
            model.RoleID = Convert.ToInt32(Request.Form["RoleID"]);
            model.IsActive = true;

            ResponseObjectForAnything responseObjectForAnything = _userRepository.Upsert(model);
            UserLoginViewModel userLoginViewModel = new UserLoginViewModel();
            userLoginViewModel.ID = _session.LoginUserID;
            if (responseObjectForAnything.ResultCode == Constants.RESPONSE_SUCCESS)
            {
                //if (!string.IsNullOrEmpty(getExistFileName))
                //    WebHelper.DeleteFile(getExistFileName, ServerSettings.PROFILEIMAGEPATH);

                _session.InitializeUserSession(userLoginViewModel.ID, userLoginViewModel);
                //WebHelper.WebHelper.SetOperationMessage(this, Constants.ALERT_SAVE, ALERTTYPE.Success, ALERTMESSAGETYPE.TextWithClose);
                return RedirectToAction("MyProfile", "Account");

            }
            else if (responseObjectForAnything.ResultCode == Constants.RESPONSE_EXISTS)
                WebHelper.WebHelper.SetOperationMessage(this, "Username already exists!", ALERTTYPE.Warning, ALERTMESSAGETYPE.TextWithClose);
            else if (responseObjectForAnything.ResultCode == Constants.RESPONCE_EMAIL_EXISTS)
                WebHelper.WebHelper.SetOperationMessage(this, "Email already exists!", ALERTTYPE.Warning, ALERTMESSAGETYPE.TextWithClose);
            else
                WebHelper.WebHelper.SetOperationMessage(this, "Unable To Perform Operation!", ALERTTYPE.Error, ALERTMESSAGETYPE.TextWithClose);

            return View(model);
        }
        #endregion
    }
}