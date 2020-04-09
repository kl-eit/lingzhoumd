using System;
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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Ling.Dashboard.Controllers
{
    public class BlogController : Controller
    {
        #region Declaration
        IBlogsRepository _blogRepository;
        UserSession _session;
        private AppSettings _appSettings { get; set; }
        #endregion

        #region Constructor
        public BlogController(IHttpContextAccessor httpContextAccessor, IConfiguration iConfiguration, IOptions<AppSettings> settings)
        {
            _blogRepository = new BlogsRepository(iConfiguration);
            _session = new UserSession(httpContextAccessor, iConfiguration);
            _appSettings = settings.Value;
            ViewBag.SelectedMenu = "Blog";
        }
        #endregion

        #region Actions
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Manage(int id = 0)
        {
            Blogs blogs = new Blogs();
            List<BlogCategory> blogCategories = new List<BlogCategory>();
            ResponseObjectForAnything responseObjectForCategory = _blogRepository.GetBlogCategoryList();
            if (responseObjectForCategory.ResultCode == Constants.RESPONSE_SUCCESS)
            {
                blogCategories = (List<BlogCategory>)responseObjectForCategory.ResultObject;
            }

            ResponseObjectForAnything responseObjectForAnything = _blogRepository.SelectByID(id);
            if (responseObjectForAnything.ResultCode == Constants.RESPONSE_SUCCESS)
            {
                blogs = (Blogs)responseObjectForAnything.ResultObject;
                blogs.CategoryList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(blogCategories, "ID", "BlogCategoryName");
            }
            return View(blogs);
        }

        [HttpPost]
        public ActionResult Manage(Blogs model)
        {
            string imageName = string.Empty;
            string sourceFilePath = string.Empty;
            string uploadedFileName = string.Empty;
            string hdfImageName = Request.Form["hdfImageName"];

            model.CreatedBy = model.ModifiedBy = _session.LoginUserID.ToString();
            string isActive = Request.Form["IsActive"];

            model.IsActive = (isActive == "on") ? true : false;
            if (Request.Form.Files != null && Request.Form.Files.Count > 0)
            {
                var uploadedFile = Request.Form.Files[0];
                sourceFilePath = _appSettings.UploadFolderName + _appSettings.BlogImagePath;

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
            model.ImageName = imageName;
            ResponseObjectForAnything responseObjectForAnything = _blogRepository.Upsert(model);
            if (responseObjectForAnything.ResultCode == Constants.RESPONSE_SUCCESS)
            {
                WebHelper.SetOperationMessage(this, Constants.ALERT_SAVE, ALERTTYPE.Success, ALERTMESSAGETYPE.TextWithClose);
                return RedirectToAction("Index");
            }
            else if (responseObjectForAnything.ResultCode == Constants.RESPONSE_SUCCESS)
            {
                WebHelper.SetOperationMessage(this, Constants.ALERT_EXISTS, ALERTTYPE.Warning, ALERTMESSAGETYPE.TextWithClose);
            }
            else
                WebHelper.SetOperationMessage(this, Constants.ALERT_ERROR, ALERTTYPE.Error, ALERTMESSAGETYPE.TextWithClose);

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            ResponseObjectForAnything responseObjectForAnything = _blogRepository.Delete(id);
            if (responseObjectForAnything.ResultCode == Constants.RESPONSE_SUCCESS)
            {
                WebHelper.SetOperationMessage(this, Constants.ALERT_DELETE, ALERTTYPE.Success, ALERTMESSAGETYPE.TextWithClose);
                return RedirectToAction("Index");
            }
            else
                WebHelper.SetOperationMessage(this, Constants.ALERT_ERROR, ALERTTYPE.Error, ALERTMESSAGETYPE.TextWithClose);
            return View();
        }

        #endregion

        #region Methods
        public List<Blogs> GetBlogs(int pPageIndex = 1, int pPageSize = 20, string pSearch = "")
        {
            List<Blogs> entityList = new List<Blogs>();
            ResponseObjectForAnything responseObjectForAnything = _blogRepository.Select(pPageIndex, pPageSize, pSearch);

            if (responseObjectForAnything.ResultCode == Constants.RESPONSE_SUCCESS)
            {
                entityList = (List<Blogs>)responseObjectForAnything.ResultObject;
            }
            return entityList;

        }
        #endregion

        #region Ajax
        public JsonResult GetBlogList()
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

            List<Blogs> blogList = GetBlogs(pageNumber, length, search);

            if (blogList != null && blogList.Count > 0)
            {
                totalRecords = blogList.FirstOrDefault().TotalCount;
                result = this.Json(new { draw = Convert.ToInt32(draw), recordsTotal = totalRecords, recordsFiltered = totalRecords, data = blogList }, new Newtonsoft.Json.JsonSerializerSettings());
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