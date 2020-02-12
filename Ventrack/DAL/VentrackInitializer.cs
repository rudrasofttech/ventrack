using System;
using System.Collections.Generic;
using Ventrack.Models;

namespace Ventrack.DAL
{
    public class VentrackInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<VentrackContext>
    {
        protected override void Seed(VentrackContext context)
        {
            var deps = new List<Department> {
                new Department{ DateCreated = DateTime.UtcNow, Name="Longwall", VentilationCapacity=115, Status= ItemStatus.Active },
                new Department{ DateCreated = DateTime.UtcNow, Name="Tailgate", VentilationCapacity=70, Status= ItemStatus.Active },
                new Department{ DateCreated = DateTime.UtcNow, Name="Maingate 13", VentilationCapacity=49, Status= ItemStatus.Active },
                new Department{ DateCreated = DateTime.UtcNow, Name="Maingate 14", VentilationCapacity=52, Status= ItemStatus.Active }
            };

            deps.ForEach(f => context.Departments.Add(f));
            context.SaveChanges();
        }
    }
}