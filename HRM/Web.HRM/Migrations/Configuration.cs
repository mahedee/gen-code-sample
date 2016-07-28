namespace Web.HRM.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Web.HRM.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Web.HRM.Models.HRMContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Web.HRM.Models.HRMContext context)
        {
            var objSE = new Designation { Name = "Software Engineer" };
            var objSSE = new Designation { Name = "Senior Engineer" };
            var objSA = new Designation { Name = "Software Archiect" };
            var objBA = new Designation { Name = "Business Analyst" };
            var objOfficer = new Designation { Name = "Officer" };
            var objSrOfficer = new Designation { Name = "Sr. Officer" };
            var objAssMgr = new Designation { Name = "Asst. Manager" };

            var objSSD = new Dept { Name = "Software Development" };
            var objIMP = new Dept { Name = "Software Implementation" };
            var objFin = new Dept { Name = "Finance & Administration" };
            var objMkt = new Dept { Name = "Sells & Marketing" };
            var objSchain = new Dept { Name = "Supply Chain" };
            var objInn = new Dept { Name = "Software Innovation" };


            var lstEmployees = new List<Employee>()
            {
                new Employee(){EmpCode = "L0001", FullName = "Tariqul Islam", NickName = "Shakil", 
                    Designation = objSE, Dept = objSSD, Phone = "01715333333", Email ="demo@gmail.com"  },

                new Employee(){EmpCode = "L0002", FullName = "Enamul Haque", NickName = "Rony", 
                    Designation = objSSE, Dept = objIMP, Phone = "01715333332", Email ="deom@gmail.com"  },

                new Employee(){EmpCode = "L0003", FullName = "Mallik Arif Ahsan", NickName = "Arif", 
                    Designation = objAssMgr, Dept = objFin, Phone = "01715333332", Email ="deom@gmail.com"  },

                new Employee(){EmpCode = "L0004", FullName = "Jafrin Islam", NickName = "Sinthi", 
                    Designation = objSSE, Dept = objSSD, Phone = "01715333334", Email ="demo@gmail.com"  },

                new Employee(){EmpCode = "L0005", FullName = "Md. Mahedee Hasan", NickName = "Mahedee", 
                    Designation = objSSE, Dept = objSSD, Phone = "01715333334", Email ="demo@gmail.com"  },

            };

            lstEmployees.ForEach(s => context.Employees.AddOrUpdate(p => p.FullName, s));
            context.SaveChanges();

            //context.Designations.AddOrUpdate(
            //    p => p.Name,
            //        new Designation { Name = "Software Engineer" },
            //        new Designation { Name = "Senior Software Engieer" },
            //        new Designation { Name = "Software Architect" },
            //        new Designation { Name = "Business Analyst" },
            //        new Designation { Name = "Project Manager" }
            //    );

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
