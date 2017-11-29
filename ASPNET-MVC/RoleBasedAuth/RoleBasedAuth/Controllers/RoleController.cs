using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RoleBasedAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoleBasedAuth.Controllers
{

    /*
     * Step 2: Refactoring
     */
    public class RoleController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        List<ApplicationUser> applicationUsers;

        UserStore<ApplicationUser> userStore;
        UserManager<ApplicationUser> userManager;

        RoleStore<IdentityRole> roleStore;
        RoleManager<IdentityRole> roleManager;


        public RoleController()
        {
            applicationUsers = context.Users.ToList();
            userStore = new UserStore<ApplicationUser>(context);
            userManager = new UserManager<ApplicationUser>(userStore);

            roleStore = new RoleStore<IdentityRole>(context);
            roleManager = new RoleManager<IdentityRole>(roleStore);
        }

        // GET: Role
        public ActionResult Index()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            List<IdentityRole> roles = roleManager.Roles.ToList();
            return View(roles);
        }

        // GET: Role/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        [HttpPost]
        public ActionResult Create(IdentityRole vRole)
        {

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            try
            {
                // TODO: Add insert logic here

                if (!roleManager.RoleExists(vRole.Name))
                {
                    roleManager.Create(vRole);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Role/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Role/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Role/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Role/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AddUserRoles()
        {
            var roles = roleManager.Roles.ToList();
            ViewBag.UserId = new SelectList(applicationUsers, "Id", "UserName");
            ViewBag.RoleId = new SelectList(roles, "Id", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult AddUserRoles(UserRolesVM userRolesVM)
        {
            //var roleStore = new RoleStore<IdentityRole>(context);
            //var roleMngr = new RoleManager<IdentityRole>(roleStore);
            var role = roleManager.Roles.Where(p => p.Id == userRolesVM.RoleId).FirstOrDefault();

            if (!userManager.IsInRole(userRolesVM.UserId, role.Name))
            {
                //Add role to the user
                userManager.AddToRole(userRolesVM.UserId, role.Name);
            }

            var roles = roleManager.Roles.ToList();
            ViewBag.UserId = new SelectList(applicationUsers, "Id", "UserName");
            ViewBag.RoleId = new SelectList(roles, "Id", "Name");

            return View();
        }

        public ActionResult _LoadUserRoles(string userId)
        {
            List<UserRolesVM> userRolesVMs = new List<UserRolesVM>();
            //if(string.IsNullOrEmpty(userId))
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            ApplicationUser user = userManager.FindById(userId);
            IList<string> roleNames;

            try
            {
                roleNames = userManager.GetRoles(userId).ToList();
            }
            catch(Exception exp)
            {
                roleNames = new List<string>();
            }
            foreach (var roleName in roleNames)
            {
                userRolesVMs.Add(new UserRolesVM() { UserId = user.Id, UserName = user.UserName, RoleName = roleName });
            }
            return PartialView(userRolesVMs);
        }
    }
}
