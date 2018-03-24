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
using webapp.Models;

namespace webapp.Controllers
{
    public class InOutTemperaturesController : ApiController
    {
        private InOutTemperatureDbContext db = new InOutTemperatureDbContext();

        // GET: api/InOutTemperatures
        public IQueryable<InOutTemperature> GetInOutTemperatures()
        {
            return db.InOutTemperatures;
        }

        // GET: api/InOutTemperatures/5
        [ResponseType(typeof(InOutTemperature))]
        public IHttpActionResult GetInOutTemperature(int id)
        {
            InOutTemperature inOutTemperature = db.InOutTemperatures.Find(id);
            if (inOutTemperature == null)
            {
                return NotFound();
            }

            return Ok(inOutTemperature);
        }

        // PUT: api/InOutTemperatures/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInOutTemperature(int id, InOutTemperature inOutTemperature)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != inOutTemperature.ID)
            {
                return BadRequest();
            }

            db.Entry(inOutTemperature).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InOutTemperatureExists(id))
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

        // POST: api/InOutTemperatures
        [ResponseType(typeof(InOutTemperature))]
        public IHttpActionResult PostInOutTemperature(InOutTemperature inOutTemperature)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.InOutTemperatures.Add(inOutTemperature);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = inOutTemperature.ID }, inOutTemperature);
        }

        // DELETE: api/InOutTemperatures/5
        [ResponseType(typeof(InOutTemperature))]
        public IHttpActionResult DeleteInOutTemperature(int id)
        {
            InOutTemperature inOutTemperature = db.InOutTemperatures.Find(id);
            if (inOutTemperature == null)
            {
                return NotFound();
            }

            db.InOutTemperatures.Remove(inOutTemperature);
            db.SaveChanges();

            return Ok(inOutTemperature);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InOutTemperatureExists(int id)
        {
            return db.InOutTemperatures.Count(e => e.ID == id) > 0;
        }
    }
}