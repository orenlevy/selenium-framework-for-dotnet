using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace TestWebSite.Controllers
{
    public class CommandsController : Controller
    {
        public ActionResult VerifyValue()
        {
            return View();
        }

        public ActionResult VerifyText()
        {
            return View();
        }

        public ActionResult ClickAndWait()
        {
            return View();
        }

        public ActionResult SendKeys()
        {
            return View();
        }

        public ActionResult TypeCommand()
        {
            return View("~/Views/Commands/Type.cshtml");
        }

        public ActionResult WaitForElementNotPresent()
        {
            return View();
        }
    }


}
