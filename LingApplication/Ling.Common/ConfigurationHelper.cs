using System;
using Microsoft.Extensions.Configuration;

namespace Ling.Common
{
    public class ConfigurationHelper
    {
        #region DECLARATIONS

        IConfiguration _configuration;

        #endregion

        #region CONSTRUCTOR

        public ConfigurationHelper(IConfiguration iConfiguration)
        {
            _configuration = iConfiguration;
        }

        #endregion

        #region METHODS

        /// <summary>
        /// THIS METHOD IS USED TO GET DB CONNECTION STRING FROM appsettings.json FILE
        /// </summary>
        /// <returns></returns>
        public string GetConnectionString()
        {
            return _configuration.GetConnectionString("DBConnectionString");
        }

        /// <summary>
        /// THIS METHOD IS USED TO GET VALUE OF APP SETTINGS BY SETTING KEY FROM appsettings.json FILE
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetAppSettingByKey<T>(string key)
        {
            var appSettingSection = _configuration.GetSection(key).Value;
            return appSettingSection == null ? default(T) : (T)Convert.ChangeType(appSettingSection, typeof(T));
        }

        #endregion
    }
}
