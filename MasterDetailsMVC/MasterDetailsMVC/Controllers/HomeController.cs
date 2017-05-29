using MasterDetailsMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MasterDetailsMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(Category objCategory)
        {
            string msg = "Data didn't save";
            if (ModelState.IsValid)
            {
                using (DemoDBContext dc = new DemoDBContext())
                {
                    dc.Order.Add(objCategory);
                    dc.SaveChanges();
                    msg = "Data Save Successfully!!";
                }
            }
            else
            {
                msg = "There is a problem to save data.";
            }
            return new JsonResult { Data = new { msg = msg } };
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}