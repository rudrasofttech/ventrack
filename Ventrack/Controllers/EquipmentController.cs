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
    public class EquipmentController : ApiController
    {
        private VentrackContext db = new VentrackContext();

        // GET api/<controller>
        public IEnumerable<Equipment> Get()
        {
            return db.Equipments.ToList();
        }

        // GET api/<controller>/5
        public Equipment Get(int id)
        {
            return db.Equipments.FirstOrDefault(t=> t.ID == id);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
            if(db.Equipments.Count(t => t.Name.Trim().ToLower() == value.Trim().ToLower()) == 0)
            {
                Equipment eqp = new Equipment() {
                    DateCreated = DateTime.UtcNow,
                    Name = value.Trim(),
                    Status = ItemStatus.Active
                };
                db.Equipments.Add(eqp);
                db.SaveChanges();
                foreach (Department d in db.Departments.Where(t => t.Status == ItemStatus.Active))
                {
                    VentReading vr = new VentReading() {
                        DateCreated = DateTime.UtcNow,
                        Status = ItemStatus.Active,
                        Department = d,
                        Equipment = eqp,
                        Ventilation = 0
                    };
                    db.VentReadings.Add(vr);
                }
                db.SaveChanges();
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
            Equipment eqp = db.Equipments.FirstOrDefault(t => t.ID == id);
            if(eqp != null)
            {
                eqp.Name = value.Trim();
                eqp.DateModified = DateTime.UtcNow;
                db.SaveChanges();
            }
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            db.VentReadings.RemoveRange(db.VentReadings.Where(t => t.Equipment.ID == id));
            db.Equipments.Remove(db.Equipments.FirstOrDefault(t => t.ID == id));
            db.SaveChanges();
        }
    }
}