using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MasterDetailsMVC.Models
{
    //public class DemoDBContext
    //{
    //    public DemoDBContext()
    //        : base("DefaultConnection")
    //    {

    //    }

    //    //public DbSet<UserProfile> UserProfiles { get; set; }
    //    //public DbSet<Category> Categories { get; set; }
    //    //public DbSet<Product> Products { get; set; }
    //    public DbSet<Order> Order { get; set; }

    //}

    public class DemoDBContext : IdentityDbContext<ApplicationUser>
    {
        public DemoDBContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static DemoDBContext Create()
        {
            return new DemoDBContext();
        }

        public DbSet<Category> Order { get; set; }
        public DbSet<Items> OrderDetails { get; set; }
    }

}