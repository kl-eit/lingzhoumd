using Ling.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ling.Domains.Helper
{
    public class WebsiteSettingHelper
    {

        public static List<WebSiteSetting> _websiteSettingList;
        public static List<HomeSlider> _homeSliderList;

        public static void ClearWebCache()
        {
            _websiteSettingList = null;
            _homeSliderList = null;
        }
        #region Browser Cache

        public static string _globalTokenID;

        public static string GlobalTokenID
        {
            get
            {
                if (string.IsNullOrEmpty(_globalTokenID))
                {
                    _globalTokenID = DateTime.Now.Ticks.ToString();
                }
                return _globalTokenID;
            }
            set
            {
                _globalTokenID = value;
            }
        }

        public static void ClearBrowserCache()
        {
            _globalTokenID = null;
        }

        #endregion
    }
}
