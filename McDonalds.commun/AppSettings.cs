using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace McDonalds.App_Start
{
    public class AppSettings
    {
        public static T ReadSetting<T>(string key, T defaultValue)
        {
            
            var result = ConfigurationManager.AppSettings[key] ;

            return string.IsNullOrWhiteSpace(result) ? defaultValue : (T)(object)(result);
        }
    }
}