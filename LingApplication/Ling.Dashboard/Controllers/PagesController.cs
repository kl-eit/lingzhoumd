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
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;


namespace Ling.Dashboard.Controllers
{
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
            ViewBag.SelectedMenu = "CMS";
        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Home(int id = 0)
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
        public ActionResult Home(CMS model)
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
    }
}