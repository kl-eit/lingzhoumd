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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Ling.Dashboard.Controllers
{
    public class FAQController : Controller
    {
        #region Declaration

        IFAQRepository _faqRepository;
        UserSession _session;
        private AppSettings _appSettings { get; set; }
        #endregion

        #region Constructor

        public FAQController(IHttpContextAccessor httpContextAccessor, IConfiguration iConfiguration, IOptions<AppSettings> settings)
        {
            _faqRepository = new FAQRepository(iConfiguration);
            _session = new UserSession(httpContextAccessor, iConfiguration);
            _appSettings = settings.Value;
        }
        #endregion

        #region Action
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Manage(int id = 0)
        {
            FAQ model = new FAQ();
            ResponseObjectForAnything responseObjectForAnything = _faqRepository.SelectByID(id);
            if (responseObjectForAnything.ResultCode == Constants.RESPONSE_SUCCESS && responseObjectForAnything.ResultObject != null)
            {
                model = (FAQ)responseObjectForAnything.ResultObject;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Manage(FAQ model)
        {
            model.CreatedBy = model.ModifiedBy = _session.LoginUserID.ToString();
            string isactive = Request.Form["IsActive"];
            model.IsActive = (isactive == "on") ? true : false;
            ResponseObjectForAnything responseObjectForAnything = _faqRepository.Upsert(model);
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
            ResponseObjectForAnything responseObjectForAnything = _faqRepository.Delete(id);
            if (responseObjectForAnything.ResultCode == Constants.RESPONSE_SUCCESS)
                WebHelper.SetOperationMessage(this, Constants.ALERT_DELETE, ALERTTYPE.Success, ALERTMESSAGETYPE.TextWithClose);
            else
                WebHelper.SetOperationMessage(this, Constants.ALERT_ERROR, ALERTTYPE.Error, ALERTMESSAGETYPE.TextWithClose);

            return RedirectToAction("Index");
        }

        public ActionResult UpdateSortOrderID(string sortedRowIDs)
        {
            ResponseObjectForAnything responseObjectForAnything = _faqRepository.UpdateSortOrderID(sortedRowIDs, _session.LoginUserID);
            return Json(responseObjectForAnything, new Newtonsoft.Json.JsonSerializerSettings());
        }
        #endregion

        #region Method
        public List<FAQ> GetAllFAQ(int pPageIndex = 1, int pPageSize = 20, string pSearchText = "", int pOrderColumn = 0, string pCurrentOrder = "asc")
        {
            ResponseObjectForAnything responseObjectForAnything = _faqRepository.Select(pPageIndex, pPageSize, pSearchText, pOrderColumn, pCurrentOrder);

            List<FAQ> exceptionLogsList = (List<FAQ>)responseObjectForAnything.ResultObject;

            return exceptionLogsList;
        }
        #endregion

        #region Ajax Request
        public JsonResult GetFAQList()
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

            List<FAQ> FAQsList = GetAllFAQ(pageNumber, length, search, sortColumn, sortOrder);

            if (FAQsList != null && FAQsList.Count > 0)
            {
                totalRecords = FAQsList.FirstOrDefault().TotalCount;
                result = this.Json(new { draw = Convert.ToInt32(draw), recordsTotal = totalRecords, recordsFiltered = totalRecords, data = FAQsList }, new Newtonsoft.Json.JsonSerializerSettings());
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