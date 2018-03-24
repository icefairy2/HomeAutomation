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
    public class WindowsController : ApiController
    {
        private WindowDbContext db = new WindowDbContext();

        // GET: api/Windows
        public IQueryable<Window> GetWindows()
        {
            return db.Windows;
        }

        // GET: api/Windows/5
        [ResponseType(typeof(Window))]
        public IHttpActionResult GetWindow(int id)
        {
            Window window = db.Windows.Find(id);
            if (window == null)
            {
                return NotFound();
            }

            return Ok(window);
        }

        // PUT: api/Windows/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWindow(int id, Window window)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != window.Id)
            {
                return BadRequest();
            }

            db.Entry(window).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WindowExists(id))
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

        // POST: api/Windows
        [ResponseType(typeof(Window))]
        public IHttpActionResult PostWindow(Window window)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Windows.Add(window);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = window.Id }, window);
        }

        // DELETE: api/Windows/5
        [ResponseType(typeof(Window))]
        public IHttpActionResult DeleteWindow(int id)
        {
            Window window = db.Windows.Find(id);
            if (window == null)
            {
                return NotFound();
            }

            db.Windows.Remove(window);
            db.SaveChanges();

            return Ok(window);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WindowExists(int id)
        {
            return db.Windows.Count(e => e.Id == id) > 0;
        }
    }
}