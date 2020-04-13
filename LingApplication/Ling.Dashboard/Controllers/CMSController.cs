using System;
using System.Collections.Generic;
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

namespace Ling.Dashboard.Controllers
{
    [Authorize]
    public class CMSController : Controller
    {
        #region Declaration
        ICMSRepository _cmsRepository;
        UserSession _session;
        private AppSettings _appSettings { get; set; }
        #endregion    

        #region Constructor
        public CMSController(IHttpContextAccessor httpContextAccessor, IConfiguration iConfiguration, IOptions<AppSettings> settings)
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
                controller.ViewBag.SelectedMenu = "CMS";
                controller.ViewBag.LoginUserAvatar = _session.LoginUserAvtar;
            }
        }
        #endregion

        #region Action
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Manage(int id = 0)
        {
            CMS model = new CMS();
            ResponseObjectForAnything responseObjectForAnything = _cmsRepository.SelectByID(id);
            if (responseObjectForAnything.ResultCode == Constants.RESPONSE_SUCCESS && responseObjectForAnything.ResultObject != null)
            {
                model = (CMS)responseObjectForAnything.ResultObject;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Manage(CMS model)
        {
            model.CreatedBy = model.ModifiedBy = _session.LoginUserID.ToString();
            string isactive = Request.Form["IsActive"];
            model.IsActive = (isactive == "on") ? true : false;
            ResponseObjectForAnything responseObjectForAnything = _cmsRepository.Upsert(model);
            if (responseObjectForAnything.ResultCode == Constants.RESPONSE_SUCCESS)
            {
                WebHelper.SetOperationMessage(this, Constants.ALERT_SAVE, ALERTTYPE.Success, ALERTMESSAGETYPE.TextWithClose);
                return RedirectToAction("Index");
            }
            else if (responseObjectForAnything.ResultCode == Constants.RESPONSE_EXISTS)
                WebHelper.SetOperationMessage(this, Constants.ALERT_EXISTS, ALERTTYPE.Warning, ALERTMESSAGETYPE.TextWithClose);
            else
                WebHelper.SetOperationMessage(this, Constants.ALERT_ERROR, ALERTTYPE.Error, ALERTMESSAGETYPE.TextWithClose);

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            ResponseObjectForAnything responseObjectForAnything = _cmsRepository.Delete(id);
            if (responseObjectForAnything.ResultCode == Constants.RESPONSE_SUCCESS)
                WebHelper.SetOperationMessage(this, Constants.ALERT_DELETE, ALERTTYPE.Success, ALERTMESSAGETYPE.TextWithClose);
            else
                WebHelper.SetOperationMessage(this, Constants.ALERT_ERROR, ALERTTYPE.Error, ALERTMESSAGETYPE.TextWithClose);

            return RedirectToAction("Index");
        }

        #endregion

        #region AJAX Actions

        public JsonResult GetCMSData()
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

            List<CMS> cmsList = GetAllCMSData(pageNumber, length, search, sortColumn, sortOrder);

            if (cmsList != null && cmsList.Count > 0)
            {
                totalRecords = cmsList.Count;
                result = this.Json(new { draw = Convert.ToInt32(draw), recordsTotal = totalRecords, recordsFiltered = totalRecords, data = cmsList }, new Newtonsoft.Json.JsonSerializerSettings());
            }
            else
            {
                result = this.Json(new { draw = Convert.ToInt32(draw), recordsTotal = totalRecords, recordsFiltered = totalRecords, data = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            }
            return result;
        }

        #endregion

        #region Method
        public List<CMS> GetAllCMSData(int pPageIndex, int pPageSize, string pSearch = "", int pOrderColumn = 0, string pCurrentOrder = "asc")
        {
            List<CMS> entityList = new List<CMS>();
            ResponseObjectForAnything responseObjectForAnything = _cmsRepository.Select(pPageIndex, pPageSize, pSearch, pOrderColumn, pCurrentOrder);

            if (responseObjectForAnything.ResultCode == Constants.RESPONSE_SUCCESS)
            {
                entityList = (List<CMS>)responseObjectForAnything.ResultObject;
            }
            return entityList;
        }

        #endregion
    }
}