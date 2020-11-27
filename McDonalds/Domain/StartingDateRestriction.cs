using McDonalds.App_Start;
using McDonalds.Constants;
using McDonalds.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace McDonalds.Domain
{
	public class StartingDateRestriction
	{
		private static bool Holidays(McDonaldsContext context, DateTime currentDate)
		{
			return context
				.Holydays
				.AsNoTracking()
				.Any(h => h.Date.Date != currentDate.Date || h.Date.AddDays(-1).Date != currentDate.Date);
		}

		private static bool DeploiementDates(McDonaldsContext context, DateTime currentDate)
		{
			throw new NotImplementedException(); // dcmaster et dcCoord
		}

		private static bool WeekDates(McDonaldsContext context, DateTime currentDate)
		{
			string[] result = AppSettings.ReadSetting(AppSettingConstant.AuthorizedWeekDate, string.Empty).Split(',');

			return result.Any(r => result.Contains(currentDate.ToString("dddd"), StringComparer.OrdinalIgnoreCase));
		}

		public static bool IsValid(McDonaldsContext context, DateTime currentDate)
		{
			return WeekDates(context, currentDate)
				|| !Holidays(context, currentDate);
		}

	}
}