using McDonalds.DAL;
using McDonalds.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace McDonalds.ApiControllers
{
    public class PrioritiesController : ApiController
    {
        private McDonaldsContext db = new McDonaldsContext();

        public IEnumerable<Restaurant> GetPriorities()
        {
            return PriorityRestriction.GetPriorityRestaurant(db);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
