using McDonalds.DAL;
using McDonalds.Domain;
using McDonalds.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace McDonalds.ApiControllers
{
    public class AuthorizationsController : ApiController
    {
        private McDonaldsContext db = new McDonaldsContext();

        // POST: api/ServerEvents
        [ResponseType(typeof(AuthorizationModel))]
        public IHttpActionResult PostAuthorization(string dcMaster, string deploymentCoord, string ipAddress, DateTime upTimes)
        {
            if (!IpAddressRestriction.IsValid(db, ipAddress, out int restaurantId))
            {
                return Ok(new AuthorizationModel()
                {
                    StatusCode = "",
                    Detail = "Addresse Ip invalide"
                });
            }

            var result = db
                .Restaurants
                .FirstOrDefault(r => r.RestaurantId == restaurantId);

            if (!TwoWeekRestartRestriction.CheckLastServerUpTimes(db, ipAddress, upTimes))
            {
                result.ServerEvents = new List<ServerEvent>()
                {
                     new ServerEvent()
                     {
                         Date = DateTime.Now,
                         Event = Event.Redemarrage,
                         Detail = "Dernier Redemarrage Serveur",
                         UpTimes = upTimes
                     }
                };

                db.SaveChanges();
            }

            if (TwoWeekRestartRestriction.IsValid(db,ipAddress,DateTime.Now))
            {
                return Ok( new AuthorizationModel()
                {
                    StatusCode = "",
                    Detail = "Le dernier demmarage est inferieur a 15 jours"
                });
            }

            if (!MaxRestartRestriction.IsValid(db,DateTime.Now))
            {
                return Ok(new AuthorizationModel()
                {
                    StatusCode = "",
                    Detail = "Vous avez depasser le nombre de redemmarage authorisé"
                });
            }

            if (!StartingDateRestriction.IsValid(db,DateTime.Now))
            {

                return Ok(new AuthorizationModel()
                {
                    StatusCode = "",
                    Detail = "Impossible d'executer un redemarrage aujourd'hui"
                });
            }
            
            return Ok(new AuthorizationModel()
            {
                StatusCode = "",
                Detail = "Vous pouvez redemarrer la machine distante"
            });
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
