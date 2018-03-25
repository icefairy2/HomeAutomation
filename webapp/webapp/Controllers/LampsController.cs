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
    public class LampsController : ApiController
    {
        private LampDbContext db = new LampDbContext();

        // GET: api/LampsToday
        public IQueryable<Lamp> GetLampsToday()
        {
            DateTime yesterday = DateTime.Now.AddDays(-1);
            // return all lamps in the last 24 hours
            return db.Lamps.Where(lamp => lamp.DateRecorded > yesterday).OrderBy(lamp => lamp.DateRecorded);
        }

        // GET: api/Lamps/5
        [ResponseType(typeof(Lamp))]
        public IHttpActionResult GetLamp(int id)
        {
            Lamp lamp = db.Lamps.Find(id);
            if (lamp == null)
            {
                return NotFound();
            }

            return Ok(lamp);
        }

        // PUT: api/Lamps/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLamp(int id, Lamp lamp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lamp.Id)
            {
                return BadRequest();
            }

            db.Entry(lamp).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LampExists(id))
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

        // POST: api/Lamps
        [ResponseType(typeof(Lamp))]
        public IHttpActionResult PostLamp(Lamp lamp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Lamps.Add(lamp);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = lamp.Id }, lamp);
        }

        // DELETE: api/Lamps/5
        [ResponseType(typeof(Lamp))]
        public IHttpActionResult DeleteLamp(int id)
        {
            Lamp lamp = db.Lamps.Find(id);
            if (lamp == null)
            {
                return NotFound();
            }

            db.Lamps.Remove(lamp);
            db.SaveChanges();

            return Ok(lamp);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LampExists(int id)
        {
            return db.Lamps.Count(e => e.Id == id) > 0;
        }
    }
}