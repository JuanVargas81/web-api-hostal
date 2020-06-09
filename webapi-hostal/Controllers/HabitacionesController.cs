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
    public class HabitacionesController : ApiController
    {
        private hostalEntities db = new hostalEntities();

        // GET: api/Habitaciones
        public IQueryable<Habitacion> GetHabitacions()
        {
            return db.Habitacions;
        }

        // GET: api/Habitaciones/5
        [ResponseType(typeof(Habitacion))]
        public IHttpActionResult GetHabitacion(int id)
        {
            Habitacion habitacion = db.Habitacions.Find(id);
            if (habitacion == null)
            {
                return NotFound();
            }

            return Ok(habitacion);
        }

        // PUT: api/Habitaciones/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHabitacion(int id, Habitacion habitacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != habitacion.id)
            {
                return BadRequest();
            }

            db.Entry(habitacion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HabitacionExists(id))
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

        // POST: api/Habitaciones
        [ResponseType(typeof(Habitacion))]
        public IHttpActionResult PostHabitacion(Habitacion habitacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Habitacions.Add(habitacion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = habitacion.id }, habitacion);
        }

        // DELETE: api/Habitaciones/5
        [ResponseType(typeof(Habitacion))]
        public IHttpActionResult DeleteHabitacion(int id)
        {
            Habitacion habitacion = db.Habitacions.Find(id);
            if (habitacion == null)
            {
                return NotFound();
            }

            db.Habitacions.Remove(habitacion);
            db.SaveChanges();

            return Ok(habitacion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HabitacionExists(int id)
        {
            return db.Habitacions.Count(e => e.id == id) > 0;
        }
    }
}