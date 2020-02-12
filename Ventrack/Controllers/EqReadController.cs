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
    public class EqReadController : ApiController
    {
        private VentrackContext db = new VentrackContext();
        // GET api/<controller>
        public EquipmentReadingModel Get()
        {
            // get the list of departments and readings from db so that we dont have to make more calls to db
            var deps = db.Departments.OrderBy(t => t.ID).ToList();
            var readings = db.VentReadings.ToList();

            //this model will store everything thing to display on UI
            EquipmentReadingModel model = new EquipmentReadingModel();
            //go through each equipment
            foreach (Equipment eqp in db.Equipments)
            {
                EquipmentReading er = new EquipmentReading();
                er.Equipment = eqp;
                
                //loop through each department
                foreach (var l in deps)
                {
                    //get reading particular to the department
                    var vr = readings.FirstOrDefault(t => t.Equipment.ID == er.Equipment.ID && t.Department.ID == l.ID);
                    DepReading dr = new DepReading() { ID = vr.ID, Dep = l, Reading = (vr != null) ? vr.Ventilation : 0 };
                    er.DepReadings.Add(dr);
                }
                model.Readings.Add(er);
            }

            //get total reading for each department and check if current reading exceeds allowed ventilation limit
            foreach(Department d in deps)
            {
                int currenttotalreading = readings.Where(t => t.Department.ID == d.ID && t.Status == ItemStatus.Active).Sum(t => t.Ventilation);
                model.Departments.Add(new Tuple<Department, int, bool>(d, currenttotalreading, currenttotalreading > d.VentilationCapacity ? true : false));
            }

            return model;
        }

        // GET api/<controller>/5
        public EquipmentReading Get(int id)
        {
            var deps = db.Departments.OrderBy(t => t.ID).ToList();
            EquipmentReading er = new EquipmentReading();
            if(id == 0) {
                //add an empty Equipment reading to show on UI
                er.Equipment = new Equipment() { ID = 0, Name = "", DateCreated = DateTime.UtcNow, Status = ItemStatus.Active };
                foreach (var l in deps)
                {
                    
                    DepReading dr = new DepReading() { Dep = l, Reading = 0 };
                    er.DepReadings.Add(dr);
                }
            }
            else
            {
                //load er from database
                //not need for test app
            }
            return er;
        }

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}