using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ventrack.DAL;
using Ventrack.Models;

namespace Ventrack.Controllers
{
    public class VentReadingController : ApiController
    {
        private VentrackContext db = new VentrackContext();

        // GET api/<controller>
        public IEnumerable<VentReading> Get()
        {
            return db.VentReadings.ToList();
        }

        // GET api/<controller>/5
        public VentReading Get(int id)
        {
            return db.VentReadings.FirstOrDefault(t => t.ID == id);
        }

        // POST api/<controller>
        public void Post([FromBody]int eqpid, [FromBody]int depid, [FromBody]int value)
        {
            if (db.VentReadings.Count(t => t.Equipment.ID == eqpid && t.Department.ID== depid) == 0)
            {
                VentReading vr = new VentReading();
                vr.Status = ItemStatus.Active;
                vr.Ventilation = value;
                vr.Equipment = db.Equipments.FirstOrDefault(t => t.ID == eqpid);
                vr.Department = db.Departments.FirstOrDefault(t => t.ID == depid);
                db.VentReadings.Add(vr);
                db.SaveChanges();
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]int value)
        {
            var vr = db.VentReadings.FirstOrDefault(t => t.ID == id);
            if (vr != null)
            {
                vr.Ventilation = value;
                db.SaveChanges();
            }
            
        }

        // DELETE api/<controller>
        public IHttpActionResult Delete([FromBody]int id)
        {
            db.VentReadings.Remove(db.VentReadings.FirstOrDefault(t => t.ID == id));
            db.SaveChanges();
            return Ok();
        }
    }
}