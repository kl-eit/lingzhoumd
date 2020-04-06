using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ling.Common;
using Ling.Dashboard.Session;
using Ling.Domains.Abstract;
using Ling.Domains.Entities;
using Ling.Domains.ResponseObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Ling.Domains.Concrete;
using Microsoft.AspNetCore.Authorization;

namespace Ling.Dashboard.Controllers
{
    [Authorize]
    public class TreatmentController : Controller
    {
        #region Declaration

        ITreatmentsRepository _treatmentsRepository;
        UserSession _session;
        private AppSettings _appSettings { get; set; }

        #endregion

        #region Constructor

        public TreatmentController(IHttpContextAccessor httpContextAccessor, IConfiguration iConfiguration, IOptions<AppSettings> settings)
        {
            _treatmentsRepository = new TreatmentsRepository(iConfiguration);
            _session = new UserSession(httpContextAccessor, iConfiguration);
            _appSettings = settings.Value;
        }

        #endregion

        #region Actions

        public IActionResult Index()
        {
            return View();
        }

        #endregion

        #region Methods

        public List<Treatments> GetTreatments(int pPageIndex = 1, int pPageSize = 20, string pSearch = "")
        {
            List<Treatments> entityList = new List<Treatments>();
            ResponseObjectForAnything responseObjectForAnything = _treatmentsRepository.Select(pPageIndex, pPageSize, pSearch);

            if (responseObjectForAnything.ResultCode == Constants.RESPONSE_SUCCESS)
            {
                entityList = (List<Treatments>)responseObjectForAnything.ResultObject;
            }
            return entityList;

        }

        #endregion
    }
}