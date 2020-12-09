using McDonalds.Commun;
using McDonalds.Constants;
using McDonalds.DAL;
using McDonalds.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using McDonalds.DAL;


namespace McDonalds.ApiControllers
{
	public class ValuesController : ApiController
	{
		private McDonaldsContext db = new McDonaldsContext();

		// GET api/values
		public IEnumerable<string> Get()
		{

			List<Restaurant> restaurantList = new List<Restaurant>();

			try
			{

				DataTable deploiementTable = ExcelParserHelper
						.ReadExcelFile(@"" + AppSettings.ReadSetting<string>(AppSettingConstant.ListRestaurantFile, default(string)))
						.Tables["Déploiement"];

				for (int i = 2; i < deploiementTable.Rows.Count; i++)
				{
					restaurantList.Add(new Restaurant()
					{
						RestaurantId = Convert.ToInt32(deploiementTable.Rows[i][0]),
						ServerIpAddress = IpAddressHelper.CcToIp(Convert.ToInt32(deploiementTable.Rows[i][0]), 71).ToString(),
						Nom = Convert.ToString(deploiementTable.Rows[i][1]),
					});
				}

				//db.Set<Restaurant>().AddOrUpdate(r => r.RestaurantId, restaurantList.ToArray());

			}
			catch (Exception ex)
			{
				throw ex;
			}
			return new string[] { "value1", "value2" };
		}

		// GET api/values/5
		public string Get(int id)
		{
			return "value";
		}

		// POST api/values
		public void Post([FromBody] string value)
		{
		}

		// PUT api/values/5
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/values/5
		public void Delete(int id)
		{
		}
	}
}
