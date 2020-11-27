using McDonalds.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace McDonalds.Domain
{
    public class IpAddressRestriction
    {
        #region [Utils] IpToCc / CcToIp

        private static IPAddress CcToIp(int cc, byte host = 0)
        {
            byte[] parts = new byte[4];

            parts[0] = 10;
            parts[1] = cc < 1536 ? Convert.ToByte((cc / 256) + 19) : Convert.ToByte((cc / 256) - 2);
            parts[2] = Convert.ToByte(cc % 256);
            parts[3] = host;
            return new IPAddress(parts);
        }

        private static int IpToCc(IPAddress ip)
        {
            int cc = 0;
            byte[] digits = ip.GetAddressBytes();

            switch (digits[1])
            {
                case 4:
                case 5:
                    cc = Convert.ToInt32(digits[2]) + 256 * (Convert.ToInt32(digits[1]) + 2);
                    break;
                case 19:
                    cc = Convert.ToInt32(digits[2]);
                    break;
                default:
                    cc = Convert.ToInt32(digits[2]) + 256 * (Convert.ToInt32(digits[1]) - 19);
                    break;
            }

            return cc;
        }

        #endregion

        public static bool IsValid(McDonaldsContext context, string ipAddress, out int restaurantId)
		{
            var result = IpToCc(IPAddress.Parse(ipAddress));

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