using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ling.Common;
using Ling.Dashboard.Session;
using Ling.Domains.Concrete;
using Ling.Domains.Entities;
using Ling.Domains.ResponseObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using static Ling.Common.Constants;

namespace Ling.Dashboard.Controllers
{
    public class ExceptionLogController : Controller
    {

        #region Declaration
        ExceptionManagerRepository _exceptionManagerRepository;
        #endregion

        #region Constructor
        public ExceptionLogController(IConfiguration iConfiguration)
        {
            _exceptionManagerRepository = new ExceptionManagerRepository(iConfiguration);
            ViewBag.SelectedMenu = "ExceptionLog";
        }
        #endregion

        #region Actions
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult SelectExceptionMessageByID(int id)
         {
            string response = string.Empty;
            if (Request.IsAjaxRequest())
            {
                ResponseObjectForAnything responseObjectForAnything = _exceptionManagerRepository.SelectByID(id);
                return Json(responseObjectForAnything);
            }
            return Content(response);
        }

        public ActionResult Delete(int id)
        {
            ResponseObjectForAnything responseObjectForAnything = _exceptionManagerRepository.Delete(id);
            if (responseObjectForAnything.ResultCode == Constants.RESPONSE_SUCCESS)
            {
                WebHelper.WebHelper.SetOperationMessage(this, Constants.ALERT_DELETE, ALERTTYPE.Success,
                    ALERTMESSAGETYPE.TextWithClose);
                return RedirectToAction("Index");
            }
            else
                WebHelper.WebHelper.SetOperationMessage(this, Constants.ALERT_ERROR, ALERTTYPE.Error,
                    ALERTMESSAGETYPE.TextWithClose);

            return View();
        }
        #endregion

        #region Method
        public List<ExceptionLog> GetAllExceptionLogs(string errorType, int pPageIndex = 1, int pPageSize = 20, string pSearchText = "", int pOrderColumn = 0, string pCurrentOrder = "asc")
        {
            ResponseObjectForAnything responseObjectForAnything = _exceptionManagerRepository.Select(pPageIndex, pPageSize, pSearchText, errorType, pOrderColumn, pCurrentOrder);

            List<ExceptionLog> exceptionLogsList = (List<ExceptionLog>)responseObjectForAnything.ResultObject;

            return exceptionLogsList;
        }
        #endregion

        #region Ajax Request
        public JsonResult GetExceptionsLogs(string errorType)
        {
            JsonResult jsonResult ;
            string search = Request.Form["search[value]"];
            string draw = Request.Form["draw"];
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            int sortColumn = Convert.ToInt32(Request.Form["order[0][column]"]);
            string sortOrder = Request.Form["order[0][dir]"];

            int totalRecords = 0;
            int pageNumber = (start + length) / length;
            int recsPerPage = length;

            List<ExceptionLog> exceptionLogList = GetAllExceptionLogs(errorType, pageNumber, length, search, sortColumn, sortOrder);
            if(exceptionLogList !=null && exceptionLogList.Count > 0)
            {
                totalRecords = exceptionLogList.FirstOrDefault().TotalCount;
                jsonResult = this.Json(new { draw = Convert.ToInt32(draw), recordsTotal = totalRecords, recordsFiltered = totalRecords, data = exceptionLogList }, new Newtonsoft.Json.JsonSerializerSettings());
            }
            else
            {
                jsonResult = this.Json(new { draw = Convert.ToInt32(draw), recordsTotal = totalRecords, recordsFiltered = totalRecords, data = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            }
            return jsonResult;
        }
        #endregion
    }
}