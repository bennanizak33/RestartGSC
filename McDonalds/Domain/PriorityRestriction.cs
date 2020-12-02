using McDonalds.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace McDonalds.Domain
{
    public class PriorityRestriction
    {
        public static IEnumerable<Restaurant> GetPriorityRestaurant(McDonaldsContext context)
        {
            return context
                .Restaurants
                .Include(r => r.ServerEvents)
                .Where(r => r.ServerEvents.OrderByDescending(se => se.Date).FirstOrDefault().Event == Event.DemandeRejete )
                .OrderBy( r => r.ServerEvents.Select(se => se.Date))
                .ToList();
        }

        public static bool CheckPriority(McDonaldsContext context, string ipAddress)
        {
            return GetPriorityRestaurant(context).FirstOrDefault(pr => pr.ServerIpAddress == ipAddress) != null;
        }
    }
}