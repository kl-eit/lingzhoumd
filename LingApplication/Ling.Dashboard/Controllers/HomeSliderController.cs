﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ling.Common;
using Ling.Dashboard.Session;
using Ling.Domains.Abstract;
using Ling.Domains.Concrete;
using Ling.Domains.Entities;
using Ling.Domains.ResponseObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using static Ling.Common.Constants;

namespace Ling.Dashboard.Controllers
{
    [Authorize]
    public class HomeSliderController : Controller
    {
        #region Declaration

        IHomeSliderRepository _homeSliderRepository;
        UserSession _session;
        private AppSettings _appSettings { get; set; }
        #endregion

        #region Constructor

        public HomeSliderController(IHttpContextAccessor httpContextAccessor, IConfiguration iConfiguration, IOptions<AppSettings> settings)
        {
            _homeSliderRepository = new HomeSliderRepository(iConfiguration);
            _session = new UserSession(httpContextAccessor, iConfiguration);
            _appSettings = settings.Value;
            
        }

        public override void  OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var controller = context.Controller as Controller;
            if (controller != null)
            {
                controller.ViewBag.SelectedMenu = "HomeSlider";
            }
        }

        #endregion

        #region Actions
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Manage(int id = 0)
        {
            HomeSlider model = new HomeSlider();
            ResponseObjectForAnything responseObjectForAnything = _homeSliderRepository.SelectByID(id);
            if (responseObjectForAnything.ResultCode == Constants.RESPONSE_SUCCESS && responseObjectForAnything.ResultObject != null)
            {
                model = (HomeSlider)responseObjectForAnything.ResultObject;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Manage(HomeSlider model)
        {
            string imageName = string.Empty;
            string videoName = string.Empty;
            string sourceFilePath = string.Empty;
            string uploadedFileName = string.Empty;

            string hdfBannerImageName = Request.Form["hdfBannerImage"];
            string hdfBannerVideoName = Request.Form["hdfBannerVideo"];

            model.CreatedBy = model.ModifiedBy = _session.LoginUserID.ToString();
            string isActive = Request.Form["IsActive"];

            model.IsActive = (isActive == "on") ? true : false;
            if (Request.Form.Files != null && Request.Form.Files.Count > 0)
            {
                var uploadedFile = Request.Form.Files[0];
                sourceFilePath = _appSettings.UploadFolderName + _appSettings.HomeSliderImagePath;
                uploadedFileName = uploadedFile.FileName;
                if (!string.IsNullOrEmpty(uploadedFileName))
                {
                    imageName = Guid.NewGuid().ToString() + "_" + uploadedFile.FileName;
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
                    if (!string.IsNullOrEmpty(hdfBannerImageName))
                    {
                        string imageVersions = Constants.THUMBNAILIMAGERESIZER + "," + Constants.LARGEIMAGERESIZER + "," + Constants.SMALLIMAGERESIZER + "," + Constants.MEDIUMIMAGERESIZER;
                        string destinationFilePath = Path.Combine(_appSettings.DashboardPhysicalUploadPath, sourceFilePath);
                        CommonHelper.ResizeImage(fileSavePath, destinationFilePath, imageName, imageVersions);
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(hdfBannerImageName))
                    imageName = hdfBannerImageName;
                else if (!string.IsNullOrEmpty(hdfBannerVideoName))
                    videoName = hdfBannerVideoName;
            }
            if (!string.IsNullOrEmpty(hdfBannerImageName))
            {
                model.ImageName = imageName;
            }
            else if (!string.IsNullOrEmpty(hdfBannerVideoName))
            {
                model.Video = imageName;
            }
            ResponseObjectForAnything responseObjectForAnything = _homeSliderRepository.Upsert(model);
            HomeSlider homeSlider = new HomeSlider();

            if (responseObjectForAnything.ResultCode == Constants.RESPONSE_SUCCESS)
            {
                WebHelper.SetOperationMessage(this, Constants.ALERT_SAVE, ALERTTYPE.Success, ALERTMESSAGETYPE.TextWithClose);
                return RedirectToAction("Index", "HomeSlider");
            }
            else
                WebHelper.SetOperationMessage(this, "Unable To Perform Operation!", ALERTTYPE.Error, ALERTMESSAGETYPE.TextWithClose);
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            ResponseObjectForAnything responseObjectForAnything = _homeSliderRepository.Delete(id);
            if (responseObjectForAnything.ResultCode == Constants.RESPONSE_SUCCESS)
            {
                WebHelper.SetOperationMessage(this, Constants.ALERT_DELETE, ALERTTYPE.Success, ALERTMESSAGETYPE.TextWithClose);
                return RedirectToAction("Index");
            }
            else
                WebHelper.SetOperationMessage(this, Constants.ALERT_ERROR, ALERTTYPE.Error, ALERTMESSAGETYPE.TextWithClose);
            return View();
        }

        public ActionResult UpdateSortOrderID(string sortedRowIDs)
        {
            ResponseObjectForAnything responseObjectForAnything = _homeSliderRepository.UpdateSortOrderID(sortedRowIDs, _session.LoginUserID);
            return Json(responseObjectForAnything, new Newtonsoft.Json.JsonSerializerSettings());
        }

        #endregion

        #region Methods

        public List<HomeSlider> GetHomeSliderList(string pSearch = "")
        {
            List<HomeSlider> entityList = new List<HomeSlider>();
            ResponseObjectForAnything responseObjectForAnything = _homeSliderRepository.Select(0, 0, pSearch);

            if (responseObjectForAnything.ResultCode == Constants.RESPONSE_SUCCESS)
            {
                entityList = (List<HomeSlider>)responseObjectForAnything.ResultObject;
            }
            return entityList;

        }

        #endregion

        #region Ajax
        public JsonResult GetHomeSlider()
        {
            JsonResult result;
            string search = Request.Form["search[value]"];
            string draw = Request.Form["draw"];
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            int sortColumn = Convert.ToInt32(Request.Form["order[0][column]"]);
            string sortOrder = Request.Form["order[0][dir]"];

            int totalRecords = 0;
            int pageNumber = (start + length) / length;
            int recsPerPage = length;

            List<HomeSlider> homeSlidersList = GetHomeSliderList(search);

            if (homeSlidersList != null && homeSlidersList.Count > 0)
            {
                totalRecords = homeSlidersList.FirstOrDefault().TotalCount;
                result = this.Json(new { draw = Convert.ToInt32(draw), recordsTotal = totalRecords, recordsFiltered = totalRecords, data = homeSlidersList }, new Newtonsoft.Json.JsonSerializerSettings());
            }
            else
            {
                result = this.Json(new { draw = Convert.ToInt32(draw), recordsTotal = totalRecords, recordsFiltered = totalRecords, data = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            }
            return result;

        }
        #endregion
    }
}