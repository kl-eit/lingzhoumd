using System;
using System.Collections.Generic;
using Ling.Common;
using Ling.Dashboard.Session;
using Ling.Domains.Abstract;
using Ling.Domains.Concrete;
using Ling.Domains.Entities;
using Ling.Domains.Helper;
using Ling.Domains.ResponseObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Ling.Dashboard.Controllers
{
    [Authorize]
    public class WebsiteSettingController : Controller
    {
        #region Declaration
        IWebSiteSettingRepository _websiteSettingRepository;
        UserSession _session;
        private AppSettings _appSettings { get; set; }
        #endregion

        #region Constructor
        public WebsiteSettingController(IHttpContextAccessor httpContextAccessor, IConfiguration iConfiguration, IOptions<AppSettings> settings)
        {
            _websiteSettingRepository = new WebsiteSettingRepository(iConfiguration);
            _session = new UserSession(httpContextAccessor, iConfiguration);
            _appSettings = settings.Value;
            ViewBag.SelectedMenu = "WebsiteSetting";
        }
        #endregion


        #region Action

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Manage(int id = 0)
        {
            WebSiteSetting model = new WebSiteSetting();
            ResponseObjectForAnything responseObjectForAnything = _websiteSettingRepository.SelectByID(id);
            string uname = _session.LoginUserName;
            if (responseObjectForAnything.ResultCode == Constants.RESPONSE_SUCCESS && responseObjectForAnything.ResultObject != null)
            {
                model = (WebSiteSetting)responseObjectForAnything.ResultObject;
                return View(model);
            }
            return RedirectToAction("Index", "WebsiteSetting");
        }

        [HttpPost]
        public ActionResult Manage(WebSiteSetting model)
        {
            model.ModifiedBy = _session.LoginUserID.ToString();
            model.ModifiedDate = DateTime.Now;
            ResponseObjectForAnything responseObjectForAnything = _websiteSettingRepository.Upsert(model);
            if (responseObjectForAnything.ResultCode == Constants.RESPONSE_SUCCESS)
            {
                WebHelper.SetOperationMessage(this, Constants.ALERT_SAVE, ALERTTYPE.Success, ALERTMESSAGETYPE.TextWithClose);
                WebsiteSettingHelper.ClearWebCache();
                WebHelper.ClearWebApplicationCache("ClearWebCache", "https://localhost:44324/");
                return RedirectToAction("Index", "WebsiteSetting");
            }
            else if (responseObjectForAnything.ResultCode == Constants.RESPONSE_EXISTS)
                WebHelper.SetOperationMessage(this, Constants.ALERT_EXISTS, ALERTTYPE.Error, ALERTMESSAGETYPE.TextWithClose);
            else
                WebHelper.SetOperationMessage(this, Constants.ALERT_ERROR, ALERTTYPE.Error, ALERTMESSAGETYPE.TextWithClose);

            return View(model);
        }

        #endregion

        #region AJAX Actions

        public JsonResult GetAllWebSiteSetting()
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

            List<WebSiteSetting> exceptionLogList = GetAllWebSiteSettings(pageNumber, length, search);

            if (exceptionLogList != null && exceptionLogList.Count > 0)
            {
                totalRecords = exceptionLogList.Count;
                result = this.Json(new { draw = Convert.ToInt32(draw), recordsTotal = totalRecords, recordsFiltered = totalRecords, data = exceptionLogList },new Newtonsoft.Json.JsonSerializerSettings());
            }
            else
            {
                result = this.Json(new { draw = Convert.ToInt32(draw), recordsTotal = totalRecords, recordsFiltered = totalRecords, data = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            }
            return result;
        }

        public ActionResult ClearBrowserCache()
        {
            WebsiteSettingHelper.ClearBrowserCache();
            WebsiteSettingHelper.ClearWebCache();
            WebHelper.ClearWebApplicationCache("ClearWebCache", "https://localhost:44324/");
            WebHelper.ClearWebApplicationCache("ClearBrowserCache", "https://localhost:44324/");
            WebHelper.SetOperationMessage(this, "Browser cache has been cleared!", ALERTTYPE.Success, ALERTMESSAGETYPE.TextWithClose);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Action will be called when user click Delete button
        /// </summary>
        /// <param name = "id" ></ param >
        /// < returns ></ returns >
        public ActionResult Delete(int id)
        {
            ResponseObjectForAnything responseObjectForAnything = _websiteSettingRepository.Delete(id);

            if (responseObjectForAnything.ResultCode == Constants.RESPONSE_SUCCESS)
            {
                WebsiteSettingHelper.ClearWebCache();
                WebHelper.ClearWebApplicationCache("ClearWebCache", "https://localhost:44324/");
                WebHelper.SetOperationMessage(this, Constants.ALERT_DELETE, ALERTTYPE.Success, ALERTMESSAGETYPE.TextWithClose);
            }
            else
                WebHelper.SetOperationMessage(this, Constants.ALERT_ERROR, ALERTTYPE.Error, ALERTMESSAGETYPE.TextWithClose);

            return RedirectToAction("Index");
        }

        public JsonResult UploadDropzoneFile()
        {
            return Json(new { isSuccess = true }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        #endregion

        #region Methods

        public List<WebSiteSetting> GetAllWebSiteSettings(int pPageIndex, int pPageSize, string pSearch = "")
        {
            ResponseObjectForAnything responseObjectForAnything = _websiteSettingRepository.Select(pPageIndex, pPageSize, pSearch);
            List<WebSiteSetting> websiteSettingList = (List<WebSiteSetting>)responseObjectForAnything.ResultObject;
            return websiteSettingList;
        }

        #endregion
    }
}