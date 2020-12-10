using McDonalds.Data.Context;
using McDonalds.Data.Models;
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
        private McDonaldsContext Context = new McDonaldsContext();

        private int RestaurantId = default(int);
        private DateTime OperationDateTime => DateTime.Now;

        // POST: api/ServerEvents
        [ResponseType(typeof(AuthorizationModel))]
        public IHttpActionResult PostAuthorization( string ipAddress, DateTime upTimes)
        {
            if (!IpAddressRestriction.IsValid(Context, ipAddress, out RestaurantId))
            {
                return Ok(new AuthorizationModel()
                {
                    StatusCode = "KO",
                    Detail = "Addresse Ip invalide"
                });
            }

            if (!TwoWeekRestartRestriction.CheckLastServerUpTimes(Context, RestaurantId, upTimes))
            {
                Context.ServerEvents.Add(new ServerEvent()
                {
                    Date = OperationDateTime,
                    Event = Event.RedemarrageOK,
                    Detail = "Dernier Redemarrage Serveur",
                    UpTimes = upTimes.Date
                });

                Context.SaveChanges();
            }

            if (!TwoWeekRestartRestriction.IsValid(Context,RestaurantId, OperationDateTime))
            {
                return Ok( new AuthorizationModel()
                {
                    StatusCode = "KO",
                    Detail = "Le dernier demmarage est inferieur a 15 jours"
                });
            }

            if (!MaxRestartRestriction.IsValid(Context, OperationDateTime))
            {
                return Ok(new AuthorizationModel()
                {
                    StatusCode = "KO",
                    Detail = "Vous avez depasser le nombre de redemmarage authorisé"
                });
            }

            if (!StartingDateRestriction.IsValid(Context, OperationDateTime))
            {

                return Ok(new AuthorizationModel()
                {
                    StatusCode = "KO",
                    Detail = "Impossible d'executer un redemarrage aujourd'hui"
                });
            }

            if (DeploiementDateRestriction.CheckDeploiementDate(RestaurantId, OperationDateTime))
            {
                return Ok(new AuthorizationModel()
                {
                    StatusCode = "KO",
                    Detail = "Impossible d'executer un redemarrage aujourd'hui, un deploiement est prevu"
                });
            }

            if (!PriorityRestriction.CheckPriority(Context, RestaurantId))
            {
                Context.ServerEvents.Add
                (
                     new ServerEvent()
                     {
                         Date = OperationDateTime,
                         Event = Event.DemandeRejete,
                         Detail = "Demande de redemarrage non prioritaire",
                         UpTimes = upTimes.Date
                     }
                );

                Context.SaveChanges();

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
                Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
