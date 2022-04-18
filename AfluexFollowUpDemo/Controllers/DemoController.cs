using AfluexFollowUpDemo.Filter;
using AfluexFollowUpDemo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AfluexFollowUpDemo.Controllers
{
    public class DemoController : Controller
    {
        // GET: Demo
        public ActionResult Demologin()
        {
            return View();
        }
    }
}
        //[HttpPost]
        //[ActionName("Demologin")]
        //[OnAction(ButtonName = "btnave")]
        //public ActionResult LoginData(Demo an) 
        //{
        //    DataSet ds = an.Demologin();
        //    if(ds!=null && ds.Tables.Count>0 &&  ds.Tables[0].Rows.Count>0)
        //    {
        //        if(ds.Tables[0].Rows[0]["Msg"]=="1")
        //        {
        //            TempData["Mgs"] = "Save Success";
        //        }
        //        else
        //        {
        //            TempData["Mgs"] = "Faiesd";
        //        }

        //    }
        //    else
        //    {
        //        TempData["Mgs"] = "try gain ";
        //    }
        //    return View(an);
        //}
        //}
//    }
//}
