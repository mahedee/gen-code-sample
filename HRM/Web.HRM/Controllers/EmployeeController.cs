using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.HRM.Models;

namespace Web.HRM.Controllers
{
    public class EmployeeController : Controller
    {
        private HRMContext db = new HRMContext();

        // GET: /Employee/
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.Dept).Include(e => e.Designation);
            return View(employees.ToList());
        }

        // GET: /Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: /Employee/Create
        public ActionResult Create()
        {
            ViewBag.DeptId = new SelectList(db.Depts, "Id", "Name");
            ViewBag.DesignationId = new SelectList(db.Designations, "Id", "Name");

            List<Dept> lstDept = db.Depts.ToList();
            ViewBag.DeptList = lstDept;

            List<Designation> lstDesignation = db.Designations.ToList();
            ViewBag.DesignationList = lstDesignation;

            return View();
        }

        // POST: /Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,EmpCode,FullName,NickName,DesignationId,DeptId,Phone,Email,Address")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.ActionDate = DateTime.Now;
                if(employee.Id != 0)
                    db.Entry(employee).State = EntityState.Modified;
                else
                    db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            ViewBag.DeptId = new SelectList(db.Depts, "Id", "Name", employee.DeptId);
            ViewBag.DesignationId = new SelectList(db.Designations, "Id", "Name", employee.DesignationId);
            return View(employee);
        }

        // GET: /Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeptId = new SelectList(db.Depts, "Id", "Name", employee.DeptId);
            ViewBag.DesignationId = new SelectList(db.Designations, "Id", "Name", employee.DesignationId);
            return View(employee);
        }

        // POST: /Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,EmpCode,FullName,NickName,DesignationId,DeptId,Phone,Email,Address,ActionDate")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeptId = new SelectList(db.Depts, "Id", "Name", employee.DeptId);
            ViewBag.DesignationId = new SelectList(db.Designations, "Id", "Name", employee.DesignationId);
            return View(employee);
        }

        // GET: /Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: /Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult _LoadSearchEmployee(string desigId, string deptId)
        {
            List<Employee> employee = new List<Employee>();
            int _desigId = 0;
            int _deptId = 0;
            Int32.TryParse(desigId, out _desigId);
            Int32.TryParse(deptId, out _deptId);

            employee = db.Employees.Where(p => (p.DeptId == _deptId || _deptId == 0) && 
                (p.DesignationId == _desigId || _desigId == 0)).ToList();

            //if (String.IsNullOrEmpty(desigId) && string.IsNullOrEmpty(deptId))
            //    employee = db.Employees.ToList();
            //else
            //{
            //    int _desigId = Convert.ToInt32(desigId);
            //    int _deptId = Convert.ToInt32(deptId);
            //    employee = db.Employees.Where(p => p.DeptId == _deptId || p.DesignationId == _desigId).ToList();
            //}

            return PartialView(employee);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
