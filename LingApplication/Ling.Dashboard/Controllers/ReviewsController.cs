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
    public class ReviewsController : Controller
    {

        #region Declaration
        IReviewsRepository _reviewsRepository;
        UserSession _session;
        private AppSettings _appSettings { get; set; }
        #endregion    

        #region Constructor
        public ReviewsController(IHttpContextAccessor httpContextAccessor, IConfiguration iConfiguration, IOptions<AppSettings> settings)
        {
            _reviewsRepository = new ReviewsRepository(iConfiguration);
            _session = new UserSession(httpContextAccessor, iConfiguration);
            _appSettings = settings.Value;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var controller = context.Controller as Controller;
            if (controller != null)
            {
                controller.ViewBag.SelectedMenu = "Reviews";
                controller.ViewBag.LoginUserAvatar = _session.LoginUserAvtar;
            }
        }
        #endregion


        #region Action
        public IActionResult Index()
        {
            return View();
        }


        public ActionResult Manage(int id=0)
        {
            Reviews reviews = new Reviews();
            ResponseObjectForAnything responseObjectForAnything = _reviewsRepository.SelectByID(id);
            if (responseObjectForAnything.ResultCode == Constants.RESPONSE_SUCCESS)
            {
                reviews = (Reviews)responseObjectForAnything.ResultObject;
            }
            return View(reviews);
        }
        [HttpPost]
        public ActionResult Manage(Reviews model)
        {
            model.CreatedBy = model.ModifiedBy = _session.LoginUserID.ToString();
            string isactive = Request.Form["IsActive"];
            model.IsActive = (isactive == "on") ? true : false;
            ResponseObjectForAnything responseObjectForAnything = _reviewsRepository.Upsert(model);
            if (responseObjectForAnything.ResultCode == Constants.RESPONSE_SUCCESS)
            {
                WebHelper.SetOperationMessage(this, Constants.ALERT_SAVE, ALERTTYPE.Success, ALERTMESSAGETYPE.TextWithClose);
                return RedirectToAction("Index");
            }
            else
                WebHelper.SetOperationMessage(this, Constants.ALERT_ERROR, ALERTTYPE.Error, ALERTMESSAGETYPE.TextWithClose);

            return View(model);
        }
        #endregion

        #region Method
        public List<Reviews> GetReviewsData(int pPageIndex, int pPageSize, string pSearch = "", int pOrderColumn = 0, string pCurrentOrder = "asc")
        {
            List<Reviews> entityList = new List<Reviews>();
            ResponseObjectForAnything responseObjectForAnything = _reviewsRepository.Select(pPageIndex, pPageSize, pSearch, pOrderColumn, pCurrentOrder);

            if (responseObjectForAnything.ResultCode == Constants.RESPONSE_SUCCESS)
            {
                entityList = (List<Reviews>)responseObjectForAnything.ResultObject;
            }
            return entityList;
        }

        #endregion
        #region Ajax Request
        public JsonResult GetReviewsList()
        {
            JsonResult jsonResult;
            string search = Request.Form["search[value]"];
            string draw = Request.Form["draw"];
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            int sortColumn = Convert.ToInt32(Request.Form["order[0][column]"]);
            string sortOrder = Request.Form["order[0][dir]"];

            int totalRecords = 0;
            int pageNumber = (start + length) / length;
            int recsPerPage = length;

            List<Reviews> reviewsList = GetReviewsData(pageNumber, length, search, sortColumn, sortOrder);

            if (reviewsList != null && reviewsList.Count > 0)
            {
                totalRecords = reviewsList.Count;

                jsonResult = this.Json(new { draw = Convert.ToInt32(draw), totalRecords = totalRecords, recordsFiltered = totalRecords, data = reviewsList }, new Newtonsoft.Json.JsonSerializerSettings());
            }
            else
            {
                jsonResult = this.Json(new { draw = Convert.ToInt32(draw), totalRecords = totalRecords, recordsFiltered = totalRecords, data = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            }
            return jsonResult;
        }
        #endregion
    }
}