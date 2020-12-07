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
        private Restaurant Restaurant = null;

        // POST: api/ServerEvents
        [ResponseType(typeof(AuthorizationModel))]
        public IHttpActionResult PostAuthorization(string ipAddress, DateTime upTimes)
        {
            if (!IpAddressRestriction.IsValid(db, ipAddress, out int restaurantId))
            {
                return Ok(new AuthorizationModel()
                {
                    StatusCode = "KO",
                    Detail = "Addresse Ip invalide"
                });
            }

            Restaurant = db
                .Restaurants
                .FirstOrDefault(r => r.RestaurantId == restaurantId);

            Restaurant.ServerEvents = new List<ServerEvent>();


            if (!TwoWeekRestartRestriction.CheckLastServerUpTimes(db, ipAddress, upTimes))
            {
                Restaurant.ServerEvents.Add(new ServerEvent()
                {
                    Date = DateTime.Now,
                    Event = Event.RedemarrageOK,
                    Detail = "Dernier Redemarrage Serveur",
                    UpTimes = upTimes
                });
            }

            db.SaveChanges();

            if (!TwoWeekRestartRestriction.IsValid(db,ipAddress,DateTime.Now))
            {
                return Ok( new AuthorizationModel()
                {
                    StatusCode = "KO",
                    Detail = "Le dernier demmarage est inferieur a 15 jours"
                });
            }

            if (!MaxRestartRestriction.IsValid(db,DateTime.Now))
            {
                return Ok(new AuthorizationModel()
                {
                    StatusCode = "KO",
                    Detail = "Vous avez depasser le nombre de redemmarage authorisé"
                });
            }

            if (!StartingDateRestriction.IsValid(db,DateTime.Now))
            {

                return Ok(new AuthorizationModel()
                {
                    StatusCode = "KO",
                    Detail = "Impossible d'executer un redemarrage aujourd'hui"
                });
            }

            if (DeploiementDateRestriction.CheckDeploiementDate(Restaurant.RestaurantId,DateTime.Now))
            {
                return Ok(new AuthorizationModel()
                {
                    StatusCode = "KO",
                    Detail = "Impossible d'executer un redemarrage aujourd'hui, un deploiement est prevu"
                });
            }

            if (!PriorityRestriction.CheckPriority(db, ipAddress))
            {
                Restaurant.ServerEvents.Add
                (
                     new ServerEvent()
                     {
                         Date = DateTime.Now,
                         Event = Event.DemandeRejete,
                         Detail = "Demande de redemarrage non prioritaire",
                         UpTimes = upTimes
                     }
                );

                db.SaveChanges();

                return Ok(new AuthorizationModel()
                {
                    StatusCode = "KO",
                    Detail = "Demande de redemarrage non prioritaire"
                });
            }

            return Ok(new AuthorizationModel()
            {
                StatusCode = "OK",
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
