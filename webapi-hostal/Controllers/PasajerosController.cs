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
using webapi_hostal.Models;

namespace webapi_hostal.Controllers
{
    public class PasajerosController : ApiController
    {
        private hostalEntities db = new hostalEntities();

        // GET: api/Pasajeros
        public IQueryable<Pasajero> GetPasajeroes()
        {
            return db.Pasajeroes;
        }

        // GET: api/Pasajeros/5
        [ResponseType(typeof(Pasajero))]
        public IHttpActionResult GetPasajero(int id)
        {
            Pasajero pasajero = db.Pasajeroes.Find(id);
            if (pasajero == null)
            {
                return NotFound();
            }

            return Ok(pasajero);
        }

        // PUT: api/Pasajeros/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPasajero(int id, Pasajero pasajero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pasajero.id)
            {
                return BadRequest();
            }

            db.Entry(pasajero).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PasajeroExists(id))
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

        // POST: api/Pasajeros
        [ResponseType(typeof(Pasajero))]
        public IHttpActionResult PostPasajero(Pasajero pasajero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pasajeroes.Add(pasajero);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pasajero.id }, pasajero);
        }

        // DELETE: api/Pasajeros/5
        [ResponseType(typeof(Pasajero))]
        public IHttpActionResult DeletePasajero(int id)
        {
            Pasajero pasajero = db.Pasajeroes.Find(id);
            if (pasajero == null)
            {
                return NotFound();
            }

            db.Pasajeroes.Remove(pasajero);
            db.SaveChanges();

            return Ok(pasajero);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PasajeroExists(int id)
        {
            return db.Pasajeroes.Count(e => e.id == id) > 0;
        }
    }
}