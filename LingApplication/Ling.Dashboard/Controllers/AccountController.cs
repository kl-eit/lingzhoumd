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
using Microsoft.AspNetCore.Authentication.Cookies;

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

        public AccountController(IHttpContextAccessor httpContextAccessor, IConfiguration iConfiguration, IOptions<AppSettings> appSettings)
        {
            _userRepository = new UserRepository(iConfiguration);
            _session = new UserSession(httpContextAccessor, iConfiguration);
            _appSettings = appSettings.Value;
        }

        #endregion

        #region Actions

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
            var a = HttpContext.User as ClaimsPrincipal;

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

                        var claims = new List<Claim>
                                            {
                                                new Claim(ClaimTypes.Name, username),
                                                new Claim(ClaimTypes.Role, user.Role),
                                                new Claim(ClaimTypes.Sid, user.ID.ToString())
                                            };

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties();
                        var userPrincipal = new ClaimsPrincipal(identity);
                        //Thread.CurrentPrincipal = userPrincipal;
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, authProperties);

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
        [Route("logout")]
        [AllowAnonymous]
        public ActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        [Route("profile")]
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

        [Route("profile")]
        [HttpPost]
        public ActionResult MyProfile(Users model)
        {
            string oldImageName = string.Empty;
            string imageName = string.Empty;
            string sourceFilePath = string.Empty;
            string uploadedFileName = string.Empty;

            model.ID = _session.LoginUserID;
            oldImageName = Request.Form["hdfOldImageName"];
            if (Request.Form.Files != null && Request.Form.Files.Count > 0)
            {
                var uploadedFile = Request.Form.Files[0];

                sourceFilePath = _appSettings.UploadFolderName + _appSettings.ProfileImagePath;
                //ServerSettings.UPLOADFOLDERNAME + ServerSettings.PROFILEIMAGEPATH;

                uploadedFileName = uploadedFile.FileName;
                if (!string.IsNullOrEmpty(uploadedFileName))
                {
                    string fileExtension = Path.GetExtension(uploadedFile.FileName);
                    imageName = Guid.NewGuid().ToString() + fileExtension;

                    string webURL = _appSettings.DashboardURL;

                    string fileDirectory = Path.Combine(_appSettings.DashboardPhysicalUploadPath, sourceFilePath);
                    if (!Directory.Exists(fileDirectory))
                    {
                        Directory.CreateDirectory(fileDirectory);
                    }

                    string fileSavePath = Path.Combine(_appSettings.DashboardPhysicalUploadPath, sourceFilePath, imageName);
                    using (var fileStream = new FileStream(fileSavePath, FileMode.Create))
                    {
                        uploadedFile.CopyTo(fileStream);
                    }
                    string imageVersions = Constants.THUMBNAILIMAGERESIZER + "," + Constants.LARGEIMAGERESIZER + "," + Constants.SMALLIMAGERESIZER + "," + Constants.MEDIUMIMAGERESIZER;
                    //imageName = WebHelper.UploadFile(imageHttpFile, sourceFilePath, imageVersions, webURL);
                    string destinationFilePath = Path.Combine(_appSettings.DashboardPhysicalUploadPath, sourceFilePath);
                    CommonHelper.ResizeImage(fileSavePath, destinationFilePath, imageName, imageVersions);
                }
            }
            else
            {
                imageName = oldImageName;
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
                if (!string.IsNullOrEmpty(oldImageName) && !string.IsNullOrEmpty(uploadedFileName))
                    WebHelper.WebHelper.DeleteFile(oldImageName, _appSettings.DashboardPhysicalUploadPath, sourceFilePath);

                _session.InitializeUserSession(userLoginViewModel.ID, userLoginViewModel);
                WebHelper.WebHelper.SetOperationMessage(this, Constants.ALERT_SAVE, ALERTTYPE.Success, ALERTMESSAGETYPE.TextWithClose);
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

        [HttpPost]
        public ActionResult ChangePassword()
        {
            string encryptedCurrentPassword = Security.Hash(Request.Form["CurrentPassword"]);
            encryptedPassword = Security.Hash(Request.Form["Password"]);
            ResponseObjectForAnything responseObjectForAnything = _userRepository.UpdatePassword(_session.LoginUserID, encryptedCurrentPassword, encryptedPassword);

            if (responseObjectForAnything.ResultCode == Constants.RESPONSE_SUCCESS)
            {
                WebHelper.WebHelper.SetOperationMessage(this, "Password changed successfully!", ALERTTYPE.Success, ALERTMESSAGETYPE.TextWithClose);
            }
            else if (responseObjectForAnything.ResultCode == Constants.RESPONSE_INVALID)
            {
                WebHelper.WebHelper.SetOperationMessage(this, "Invalid current password.", ALERTTYPE.Warning, ALERTMESSAGETYPE.TextWithClose);
            }
            else if (responseObjectForAnything.ResultCode == Constants.RESPONSE_INVALID)
            {
                WebHelper.WebHelper.SetOperationMessage(this, responseObjectForAnything.ResultMessage, ALERTTYPE.Error, ALERTMESSAGETYPE.TextWithClose);
            }

            return RedirectToAction("MyProfile", "Account");
        }
        #endregion
    }
}