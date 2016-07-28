using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Web.HRM.Models
{
    //public class HRMContext
    //{

    //}

    public class HRMContext : IdentityDbContext<ApplicationUser>
    {
        public HRMContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static HRMContext Create()
        {
            return new HRMContext();
        }

        public DbSet<Dept> Depts { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}