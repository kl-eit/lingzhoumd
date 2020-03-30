using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ling.Common
{
    public class ServerSettings
    {
        //public static string WEBURL = System.Configuration.ConfigurationManager.AppSettings["WebURL"];
        //public static string WEBPHYSICALUPLOADPATH = System.Configuration.ConfigurationManager.AppSettings["WebPhysicalUploadPath"];
        //public static string UPLOADFOLDERNAME = System.Configuration.ConfigurationManager.AppSettings["UploadFolderName"];
        //public static string PROFILEIMAGEPATH = System.Configuration.ConfigurationManager.AppSettings["ProfileImagePath"];
        //public static string ROOMIMAGEPATH = System.Configuration.ConfigurationManager.AppSettings["RoomImagePath"];
        //public static string EVENTIMAGEPATH = System.Configuration.ConfigurationManager.AppSettings["EventImagePath"];
        //public static string AMENITIESIMAGEPATH = System.Configuration.ConfigurationManager.AppSettings["AmenitiesImagePath"];
        //public static string HOMESLIDERIMAGEPATH = System.Configuration.ConfigurationManager.AppSettings["HomeSliderImagePath"];
        //public static string ADMINEMAIL = System.Configuration.ConfigurationManager.AppSettings["AdminEmail"];
        //public static string OFFERIMAGEPATH = System.Configuration.ConfigurationManager.AppSettings["OfferImagePath"];
        //public static string ENABLESSL = System.Configuration.ConfigurationManager.AppSettings["EnableSSL"];
        //public static string ADMINURL = System.Configuration.ConfigurationManager.AppSettings["AdminURL"];
        //public static string PACKAGESIMAGEPATH = System.Configuration.ConfigurationManager.AppSettings["PackagesImagePath"];
        //public static string CAPTCHASITEKEY = System.Configuration.ConfigurationManager.AppSettings["CaptchaSiteKey"];

        #region Host
        private static string _WebApplicationURL = string.Empty;
        //public static string WebApplicationURL
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(_WebApplicationURL))
        //        {
        //            _WebApplicationURL = string.Concat(System.Web.HttpContext.Current.Request.Url.Scheme, "://", System.Web.HttpContext.Current.Request.Url.Authority);
        //        }
        //        return _WebApplicationURL;
        //    }
        //}

        #endregion
    }
}
