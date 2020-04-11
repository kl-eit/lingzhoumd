using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ling.Common;
using Ling.Dashboard.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Ling.Dashboard.Controllers
{
    public class CommonController : Controller
    {

        #region Declaration
        UserSession _session;
        private AppSettings _appSettings { get; set; }

        #endregion

        #region Constructor

        public CommonController(IHttpContextAccessor httpContextAccessor, IConfiguration iConfiguration, IOptions<AppSettings> settings)
        {
            _session = new UserSession(httpContextAccessor, iConfiguration);
            _appSettings = settings.Value;
        }

        #endregion

        public IActionResult Index()
        {
            return View();
        }
        #region Method

        public string UploadImage()
        {
            string generatedImage = string.Empty;
            IFormFile UploadImage = Request.Form.Files[0];
            string pBlobFileName = string.Empty;
            IFormFile hpf = Request.Form.Files[0];
            string filePath = UploadCommonFile(UploadImage, "image", out generatedImage);
            return string.Format("<img src=\"{0}\" style=\"max-width:100%;\"/>", filePath);
        }

        public string UploadVideo(string width, string align)
        {
            var videoMargin = string.Empty;
            if (align == "left")
            {
                videoMargin = "margin-right:10px;";
            }
            else if (align == "right")
            {
                videoMargin = "margin-left:10px;";
            }
            var shortCode = string.Empty;
            string generatedVideo = string.Empty;
            IFormFile UploadVideo = Request.Form.Files[0];

            string fileName = Path.GetFileName(UploadVideo.FileName);

            string pBlobFileName = string.Empty;
            string filePath = UploadCommonFile(UploadVideo, "video", out generatedVideo);
            if (!string.IsNullOrEmpty(width))
            {
                width = width + "px";
                shortCode = string.Format("<video src=\"{0}\" style=\"width:" + width + ";float:" + align + ";" + videoMargin + "\" id=" + generatedVideo + " controls></video>", filePath);
            }
            else
            {
                shortCode = string.Format("<video src=\"{0}\" style=\"width:100%;float:" + align + ";\" id=" + generatedVideo + " controls></video>", filePath);
            }
            return shortCode;
        }

        public string UploadFile()
        {
            string generatedFileName = string.Empty;
            IFormFile UploadFile = Request.Form.Files[0];
            string filePath = UploadCommonFile(UploadFile, "file", out generatedFileName);


            string storeFilePath = _appSettings.DashboardPhysicalUploadPath +_appSettings.UploadFolderName+ _appSettings.CommonFilePath+ generatedFileName;

            var sysFile = new FileInfo(storeFilePath);
            string fileDescription = string.Format("{0} ({1})", sysFile.Name, CommonHelper.SizeFormat(sysFile.Length, "N"));
            return string.Format("<p><a href=\"{0}\">{1}</a></p>", filePath, fileDescription);
        }

        private string UploadCommonFile(IFormFile UploadedFile, string FileType, out string fileName)
        {
            fileName = WebHelper.UploadFile(UploadedFile, _appSettings.UploadFolderName + _appSettings.CommonFilePath, _appSettings.DashboardPhysicalUploadPath);
            return _appSettings.DashboardURL +_appSettings.UploadFolderName + _appSettings.CommonFilePath + fileName;
        }
        #endregion
    }
}