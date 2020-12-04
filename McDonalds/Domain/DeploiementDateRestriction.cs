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
            string fichier = @"C:\Users\BENNANI Zakaria\Source\repos\RestartGSC\McDonalds\App_Data\PlanificationDeploiement.xlsx";

            return ExcelParserHelper.CheckDeploimentDateFromExcelFile(fichier, restaurantId, currentDate).DeploimentDate.HasValue ;
        }
    }
}