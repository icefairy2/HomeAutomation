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
    public class HeatsController : ApiController
    {
        private HeatDbContext db = new HeatDbContext();

        // GET: api/Heats
        public IQueryable<Heat> GetHeats()
        {
            return db.Heats;
        }

        // GET: api/Heats/5
        [ResponseType(typeof(Heat))]
        public IHttpActionResult GetHeat(int id)
        {
            Heat heat = db.Heats.Find(id);
            if (heat == null)
            {
                return NotFound();
            }

            return Ok(heat);
        }

        // PUT: api/Heats/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHeat(int id, Heat heat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != heat.Id)
            {
                return BadRequest();
            }

            db.Entry(heat).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HeatExists(id))
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

        // POST: api/Heats
        [ResponseType(typeof(Heat))]
        public IHttpActionResult PostHeat(Heat heat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Heats.Add(heat);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = heat.Id }, heat);
        }

        // DELETE: api/Heats/5
        [ResponseType(typeof(Heat))]
        public IHttpActionResult DeleteHeat(int id)
        {
            Heat heat = db.Heats.Find(id);
            if (heat == null)
            {
                return NotFound();
            }

            db.Heats.Remove(heat);
            db.SaveChanges();

            return Ok(heat);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HeatExists(int id)
        {
            return db.Heats.Count(e => e.Id == id) > 0;
        }
    }
}