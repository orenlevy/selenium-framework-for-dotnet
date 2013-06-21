using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestWebSite.Controllers
{
    public class RunSimpleScriptController : Controller
    {
        //
        // GET: /RunSimpleScript/

        public ActionResult Step1()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Step2(string firstName, string lastName)
        {
            ViewBag.FirstName = firstName;
            ViewBag.LastName = lastName;
            return View();
        }
    }
}
