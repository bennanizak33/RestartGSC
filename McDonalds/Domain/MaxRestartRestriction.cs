using McDonalds.App_Start;
using McDonalds.Constants;
using McDonalds.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace McDonalds.Domain
{
	public class MaxRestartRestriction
	{
		public static bool IsValid(McDonaldsContext context, DateTime dateTime)
		{
			return context
				.ServerEvents
				.AsNoTracking()
				.Where(se => se.Date.Date == dateTime.Date && se.Event == Event.RedemarrageOK)
				.Count() < AppSettings.ReadSetting(AppSettingConstant.MaxRestartPerDay, 0);
		}
    }
}