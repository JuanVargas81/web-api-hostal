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
    public class MetodoPagosController : ApiController
    {
        private hostalEntities db = new hostalEntities();

        // GET: api/MetodoPagos
        public IQueryable<MetodoPago> GetMetodoPagoes()
        {
            return db.MetodoPagoes;
        }

        // GET: api/MetodoPagos/5
        [ResponseType(typeof(MetodoPago))]
        public IHttpActionResult GetMetodoPago(int id)
        {
            MetodoPago metodoPago = db.MetodoPagoes.Find(id);
            if (metodoPago == null)
            {
                return NotFound();
            }

            return Ok(metodoPago);
        }

        // PUT: api/MetodoPagos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMetodoPago(int id, MetodoPago metodoPago)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != metodoPago.id)
            {
                return BadRequest();
            }

            db.Entry(metodoPago).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MetodoPagoExists(id))
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

        // POST: api/MetodoPagos
        [ResponseType(typeof(MetodoPago))]
        public IHttpActionResult PostMetodoPago(MetodoPago metodoPago)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MetodoPagoes.Add(metodoPago);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = metodoPago.id }, metodoPago);
        }

        // DELETE: api/MetodoPagos/5
        [ResponseType(typeof(MetodoPago))]
        public IHttpActionResult DeleteMetodoPago(int id)
        {
            MetodoPago metodoPago = db.MetodoPagoes.Find(id);
            if (metodoPago == null)
            {
                return NotFound();
            }

            db.MetodoPagoes.Remove(metodoPago);
            db.SaveChanges();

            return Ok(metodoPago);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MetodoPagoExists(int id)
        {
            return db.MetodoPagoes.Count(e => e.id == id) > 0;
        }
    }
}