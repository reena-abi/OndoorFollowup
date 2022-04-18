using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AfluexFollowUpDemo.Controllers
{
    public class AdminController : AdminBaseController
    {
        // GET: Admin
        public ActionResult DashBoard()
        {
            return View();
        }
        public ActionResult EmployeeProfile()
        {
            return View();
        }
        //public ActionResult Logout()
        //{
        //    Session.Abandon();
        //    return RedirectToAction("Index", "Home");
        //}
    }
}