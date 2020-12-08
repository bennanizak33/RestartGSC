using McDonalds.Commun;
using McDonalds.Constants;
using McDonalds.DAL;
using McDonalds.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace McDonalds.Migrations.Seeds
{
    public class RestaurantsSeed
    {
        public static void Restaurants(McDonaldsContext context)
        {
            List<Restaurant> restaurantList = new List<Restaurant>();

            try
            {
                DataTable deploiementTable = ExcelParserHelper
                        .ReadExcelFile(@"" +AppSettings.ReadSetting<string>(AppSettingConstant.ListRestaurantFile, default(string)))
                        .Tables["Déploiement"];

                for (int i = 2; i < deploiementTable.Rows.Count; i++)
                {
                    restaurantList.Add(new Restaurant()
                    {
                        RestaurantId = Convert.ToInt32(deploiementTable.Rows[i][0]),
                        ServerIpAddress = IpAddressHelper.CcToIp(Convert.ToInt32(deploiementTable.Rows[i][0])).ToString(),
                        Nom = Convert.ToString(deploiementTable.Rows[i][1]),
                        OpeningDate = DateTime.Now
                    });
                }

                context.Set<Restaurant>().AddOrUpdate(r => r.RestaurantId, restaurantList.ToArray());

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}