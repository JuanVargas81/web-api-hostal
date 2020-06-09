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
    public class TarifasController : ApiController
    {
        private hostalEntities db = new hostalEntities();

        // GET: api/Tarifas
        public IQueryable<Tarifa> GetTarifas()
        {
            return db.Tarifas;
        }

        // GET: api/Tarifas/5
        [ResponseType(typeof(Tarifa))]
        public IHttpActionResult GetTarifa(int id)
        {
            Tarifa tarifa = db.Tarifas.Find(id);
            if (tarifa == null)
            {
                return NotFound();
            }

            return Ok(tarifa);
        }

        // PUT: api/Tarifas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTarifa(int id, Tarifa tarifa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tarifa.id)
            {
                return BadRequest();
            }

            db.Entry(tarifa).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TarifaExists(id))
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

        // POST: api/Tarifas
        [ResponseType(typeof(Tarifa))]
        public IHttpActionResult PostTarifa(Tarifa tarifa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tarifas.Add(tarifa);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tarifa.id }, tarifa);
        }

        // DELETE: api/Tarifas/5
        [ResponseType(typeof(Tarifa))]
        public IHttpActionResult DeleteTarifa(int id)
        {
            Tarifa tarifa = db.Tarifas.Find(id);
            if (tarifa == null)
            {
                return NotFound();
            }

            db.Tarifas.Remove(tarifa);
            db.SaveChanges();

            return Ok(tarifa);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TarifaExists(int id)
        {
            return db.Tarifas.Count(e => e.id == id) > 0;
        }
    }
}