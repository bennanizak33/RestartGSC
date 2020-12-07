using McDonalds.DAL;
using McDonalds.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace McDonalds.Domain
{
    public class IpAddressRestriction
    {
        public static bool IsValid(McDonaldsContext context, string ipAddress, out int restaurantId)
		{
            var result = IpAddressHelper.IpToCc(IPAddress.Parse(ipAddress));

            if (result  != default(int))
            {
                restaurantId = result;

                return context
                .Restaurants
                .Any(r => r.RestaurantId.Equals(result));
            }

            restaurantId = 0;

            return false;
		}
	}
}