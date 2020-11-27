using McDonalds.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace McDonalds.Domain
{
    public class TwoWeekRestartRestriction
    {
		public static bool CheckLastServerUpTimes(McDonaldsContext context, string ipAddress, DateTime upTimes)
        {
			return context
				.Restaurants
				.Include(r => r.ServerEvents)
				.Any(r => r.ServerIpAddress == ipAddress && r.ServerEvents.Any(se => se.UpTimes == upTimes));
		}
		public static bool IsValid(McDonaldsContext context, string ipAddress, DateTime dateTime)
		{

			DateTime days = DateTime.Now.AddDays(-15);

			return context
				.Restaurants
				.Include(r => r.ServerEvents)
				.Any(r => r.ServerIpAddress == ipAddress && r.ServerEvents.Max(se => se.Date) > days);
		}
	}
}