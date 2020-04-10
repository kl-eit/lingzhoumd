using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ling.Common;
using Ling.Dashboard.Session;
using Ling.Domains.Abstract;
using Ling.Domains.Concrete;
using Ling.Domains.Entities;
using Ling.Domains.ResponseObject;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Filters;
using Ling.Domains.ViewModel;
using Microsoft.AspNetCore.Authorization;
using System.IO;

namespace Ling.Dashboard.Controllers
{
    [Authorize]
    public class PagesController : Controller
    {

        #region Declaration
        ICMSRepository _cmsRepository;
        UserSession _session;
        private AppSettings _appSettings { get; set; }
        #endregion    

        #region Constructor
        public PagesController(IHttpContextAccessor httpContextAccessor, IConfiguration iConfiguration, IOptions<AppSettings> settings)
        {
            _cmsRepository = new CMSRepository(iConfiguration);
            _session = new UserSession(httpContextAccessor, iConfiguration);
            _appSettings = settings.Value;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var controller = context.Controller as Controller;
            if (controller != null)
            {
                controller.ViewBag.SelectedMenu = "Pages";
            }
        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Home()
        {
            ProfileViewModel model = new ProfileViewModel();
            ResponseObjectForAnything responseObjectForAnything = _cmsRepository.SelectProfile();
            if (responseObjectForAnything.ResultCode == Constants.RESPONSE_SUCCESS && responseObjectForAnything.ResultObject != null)
            {
                model = (ProfileViewModel)responseObjectForAnything.ResultObject;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Home(ProfileViewModel model)
        {
            string imageName = string.Empty;
            string sourceFilePath = string.Empty;
            string uploadedFileName = string.Empty;
            string hdfImageName = Request.Form["hdfImageName"];

            model.ModifiedBy = _session.LoginUserID.ToString();

            model.ModifiedDate = DateTime.Now;
            if (Request.Form.Files != null && Request.Form.Files.Count > 0)
            {
                var uploadedFile = Request.Form.Files[0];
                sourceFilePath = _appSettings.UploadFolderName + _appSettings.ProfileImagePath;

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

                    string destinationFilePath = Path.Combine(_appSettings.DashboardPhysicalUploadPath, sourceFilePath);
                    CommonHelper.ResizeImage(fileSavePath, destinationFilePath, imageName, imageVersions);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(hdfImageName))
                    imageName = hdfImageName;
            }

            model.ProfileImage = imageName;

            ResponseObjectForAnything responseObjectForAnything = _cmsRepository.ProfileUpsert(model);

            if (responseObjectForAnything.ResultCode == Constants.RESPONSE_SUCCESS)
            {
                WebHelper.SetOperationMessage(this, Constants.ALERT_SAVE, ALERTTYPE.Success, ALERTMESSAGETYPE.TextWithClose);
                return RedirectToAction("Home", "pages");
            }
          else
                WebHelper.SetOperationMessage(this, Constants.ALERT_ERROR, ALERTTYPE.Error, ALERTMESSAGETYPE.TextWithClose);

            return View(model);
        }
    }
}