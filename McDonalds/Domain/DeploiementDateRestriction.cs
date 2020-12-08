using McDonalds.Commun;
using McDonalds.Constants;
using McDonalds.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace McDonalds.Domain
{
    public class DeploiementDateRestriction
    {
        public static bool CheckDeploiementDate(int restaurantId, DateTime currentDate)
        {
            return ExcelParserHelper
                .CheckDeploimentDateFromExcelFile
                (
                    AppSettings.ReadSetting<string>(AppSettingConstant.ListRestaurantFile, default(string)),
                    restaurantId,
                    currentDate
                )
                .DeploimentDate.HasValue;
        }
    }
}