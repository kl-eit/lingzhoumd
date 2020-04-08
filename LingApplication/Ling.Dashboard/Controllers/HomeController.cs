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
using Ling.Domains.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Ling.Dashboard.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        #region Declaration
        IDashboardRepository _dashboardRepository;
        UserSession _session;
        private AppSettings _appSettings { get; set; }
        #endregion

        public HomeController(IHttpContextAccessor httpContextAccessor, IConfiguration iConfiguration, IOptions<AppSettings> settings)
        {
            _dashboardRepository = new DashboardRepository(iConfiguration);
            _session = new UserSession(httpContextAccessor, iConfiguration);
            _appSettings = settings.Value;
            ViewBag.SelectedMenu = "Dashboard";
        }

        public IActionResult Index()
        {
            ViewBag.LoginUserName = _session.LoginUserName;
            DashboardViewModel model = new DashboardViewModel();
            ResponseObjectForAnything responseObjectForAnything = _dashboardRepository.FAQ_Inquiry_Blog_Count();
            if (responseObjectForAnything.ResultCode == Constants.RESPONSE_SUCCESS)
            {
                model = (DashboardViewModel)responseObjectForAnything.ResultObject;
            }
            return View(model);
        }

        #region Methods
        public List<ContactInquiry> GetContactInquiries(int pPageIndex = 1, int pPageSize = 20, string pSearch = "", int pSortColumn = 0, string pSortOrder = "")
        {
            List<ContactInquiry> entityList = new List<ContactInquiry>();
            ResponseObjectForAnything responseObjectForAnything = _dashboardRepository.GetContactInquiry(pPageIndex, pPageSize, pSearch, pSortColumn, pSortOrder);

            if (responseObjectForAnything.ResultCode == Constants.RESPONSE_SUCCESS)
            {
                entityList = (List<ContactInquiry>)responseObjectForAnything.ResultObject;
            }
            return entityList;

        }
        #endregion

        #region Ajax
        [HttpPost]
        public JsonResult GetContactInquiryList()
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

            List<ContactInquiry> contactInquiryList = GetContactInquiries(pageNumber, length, search, sortColumn, sortOrder);

            if (contactInquiryList != null && contactInquiryList.Count > 0)
            {
                totalRecords = contactInquiryList.FirstOrDefault().TotalCount;
                result = this.Json(new { draw = Convert.ToInt32(draw), recordsTotal = totalRecords, recordsFiltered = totalRecords, data = contactInquiryList }, new Newtonsoft.Json.JsonSerializerSettings());
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