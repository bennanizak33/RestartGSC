using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using McDonalds.DAL;

namespace McDonalds.ApiControllers
{
    public class ServerEventsController : ApiController
    {
        private McDonaldsContext db = new McDonaldsContext();

        public IQueryable<ServerEvent> GetServerEvents()
        {
            return db.ServerEvents;
        }


        [ResponseType(typeof(ServerEvent))]
        public IHttpActionResult GetServerEvent(int id)
        {
            ServerEvent serverEvent = db.ServerEvents.Find(id);
            if (serverEvent == null)
            {
                return NotFound();
            }

            return Ok(serverEvent);
        }


        [ResponseType(typeof(void))]
        public IHttpActionResult PutServerEvent(int id, ServerEvent serverEvent)
        {
            if (serverEvent.Restaurant == null)
            {
                return BadRequest("l'objet Restaurant est null");
            }

            if (id != serverEvent.ServerEventId)
            {
                return BadRequest();
            }

            db.Entry(serverEvent).State = EntityState.Modified;
            db.Entry(serverEvent.Restaurant).State = EntityState.Unchanged;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServerEventExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(ServerEvent))]
        public IHttpActionResult PostServerEvent(ServerEvent serverEvent)
        {
            if (serverEvent.Restaurant == null)
            {
                return BadRequest("l'objet Restaurant est null");
            }

            db.Entry(serverEvent.Restaurant).State = EntityState.Unchanged;

            db.ServerEvents.Add(serverEvent);
            db.SaveChanges();

            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                return BadRequest("Un probleme est survenu");
            }

            return Ok(serverEvent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ServerEventExists(int id)
        {
            return db.ServerEvents.Count(e => e.ServerEventId == id) > 0;
        }
    }
}