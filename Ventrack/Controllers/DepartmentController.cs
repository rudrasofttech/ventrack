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
    public class DepartmentController : ApiController
    {
        private VentrackContext db = new VentrackContext();

        // GET api/<controller>
        public IEnumerable<Department> Get()
        {
            return db.Departments.OrderBy(t => t.ID);
        }

        // GET api/<controller>/5
        public Department Get(int id)
        {
            return db.Departments.FirstOrDefault(t => t.ID == id);
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