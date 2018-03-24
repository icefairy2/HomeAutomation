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
    public class TemperatureRecordsController : ApiController
    {
        private TemperatureDbContext db = new TemperatureDbContext();

        // GET: api/TemperatureRecords
        public IQueryable<TemperatureRecord> GetTemperatureRecords()
        {
            return db.TemperatureRecords;
        }

        // GET: api/TemperatureRecords/5
        [ResponseType(typeof(TemperatureRecord))]
        public IHttpActionResult GetTemperatureRecord(int id)
        {
            TemperatureRecord temperatureRecord = db.TemperatureRecords.Find(id);
            if (temperatureRecord == null)
            {
                return NotFound();
            }

            return Ok(temperatureRecord);
        }

        // PUT: api/TemperatureRecords/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTemperatureRecord(int id, TemperatureRecord temperatureRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != temperatureRecord.ID)
            {
                return BadRequest();
            }

            db.Entry(temperatureRecord).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemperatureRecordExists(id))
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

        // POST: api/TemperatureRecords
        [ResponseType(typeof(TemperatureRecord))]
        public IHttpActionResult PostTemperatureRecord(TemperatureRecord temperatureRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TemperatureRecords.Add(temperatureRecord);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = temperatureRecord.ID }, temperatureRecord);
        }

        // DELETE: api/TemperatureRecords/5
        [ResponseType(typeof(TemperatureRecord))]
        public IHttpActionResult DeleteTemperatureRecord(int id)
        {
            TemperatureRecord temperatureRecord = db.TemperatureRecords.Find(id);
            if (temperatureRecord == null)
            {
                return NotFound();
            }

            db.TemperatureRecords.Remove(temperatureRecord);
            db.SaveChanges();

            return Ok(temperatureRecord);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TemperatureRecordExists(int id)
        {
            return db.TemperatureRecords.Count(e => e.ID == id) > 0;
        }
    }
}